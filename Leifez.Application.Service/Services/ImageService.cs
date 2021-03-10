using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Leifez.Application.Service.Services
{
    public class ImageService : IImageService
    {
        private const int Md5FolderNameLength = 3;
        private const int Md5FolderMaxQuantityFiles = 1000;

        private readonly DirectoryInfo RootDirectoryInfo = new DirectoryInfo("files");
        private readonly IImageDomain _imageDomain;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageService> _logger;

        public ImageService(
            IImageDomain imageDomain,
            IMapper mapper,
            ILogger<ImageService> logger)
        {
            _mapper = mapper;
            _imageDomain = imageDomain;
            _logger = logger;
        }

        private Image Base64ToImage(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                var image = Image.FromStream(ms, true);
                return image;
            }
        }

        private string ImageToBase64(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private string ImageToMd5(Image image)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes;
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    inputBytes = ms.ToArray();
                }

                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private string GetHashPart(string hash, int offset)
        {
            if (offset > hash.Length - Md5FolderNameLength)
            {
                _logger.LogCritical("The search exceeded the length of the file hash. Saving is not possible!");
                return string.Empty;
            }

            return hash.Substring(offset, Md5FolderNameLength);
        }

        /// <summary>
        /// If a file with this <param name="hash"> exists, then returns the path to the file. 
        /// If a file does not exist, returns the path to save the file
        /// </summary>
        /// <param name="currentDirectoryInfo">Search directory</param>
        /// <param name="hash">Hash aka file name</param>
        /// <param name="offset">Offset part of hash for folder name  </param>
        /// <returns></returns>
        private FileSystemInfo RecursiveSearch(DirectoryInfo currentDirectoryInfo, string hash, int offset = 0)
        {
            var md5Part = GetHashPart(hash, offset);

            if (currentDirectoryInfo.FullName == RootDirectoryInfo.FullName)
            {
                currentDirectoryInfo.Create();
                var directoryInfo = currentDirectoryInfo.GetDirectories().Where(d => d.Name == md5Part).FirstOrDefault();

                if (directoryInfo != default(DirectoryInfo))
                {
                    return RecursiveSearch(directoryInfo, hash, offset + Md5FolderNameLength);
                }

                var subDirectoryInfo = currentDirectoryInfo.CreateSubdirectory(md5Part);
                return RecursiveSearch(subDirectoryInfo, hash, offset + Md5FolderNameLength);
            }

            var filesInfo = currentDirectoryInfo.GetFiles();
            var directoriesInfo = currentDirectoryInfo.GetDirectories();

            if (filesInfo.Length == Md5FolderMaxQuantityFiles)
            {
                filesInfo.ToList().ForEach(f => 
                {
                    var localMd5Part = GetHashPart(Path.GetFileNameWithoutExtension(f.Name), offset);
                    var localDirectory = currentDirectoryInfo.CreateSubdirectory(localMd5Part);
                    f.MoveTo($@"{localDirectory.FullName}\{f.Name}");
                });
            }

            if (filesInfo.Length == 0 && directoriesInfo.Length != 0)
            {
                var directoryInfo = currentDirectoryInfo.GetDirectories().Where(d => d.Name == md5Part).FirstOrDefault();

                if (directoryInfo != default(DirectoryInfo))
                {
                    return RecursiveSearch(directoryInfo, hash, offset + Md5FolderNameLength);
                }

                var subDirectoryInfo = currentDirectoryInfo.CreateSubdirectory(md5Part);

                return RecursiveSearch(subDirectoryInfo, hash, offset + Md5FolderNameLength);
            }

            var fileInfo = filesInfo.Where(f => Path.GetFileNameWithoutExtension(f.Name) == hash).FirstOrDefault();

            if (fileInfo != default(FileInfo))
            {
                return fileInfo;
            }

            return currentDirectoryInfo;
        }

        public string AddImage(string imageBase64)
        {
            var image = Base64ToImage(imageBase64);
            var imageModel = new DbImage()
            {
                Guid = Guid.NewGuid().ToString(),
                Hash = ImageToMd5(image)
            };

            if (!_imageDomain.AddImage(imageModel))
            {
                return string.Empty;
            }

            try
            {
                var result = RecursiveSearch(RootDirectoryInfo, imageModel.Hash);
                if (result is DirectoryInfo)
                {
                    var directoryInfo = result as DirectoryInfo;
                    var extension = image.RawFormat.ToString().ToLower();
                    image.Save($@"{directoryInfo.FullName}\{imageModel.Hash}.{extension}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return string.Empty;
            }

            return imageModel.Guid;
        }

        public bool DeleteImage(string guid)
        {
            throw new System.NotImplementedException();
        }

        public string GetImage(string guid)
        {
            var dbImage = _imageDomain.GetImage(guid);
            var result = RecursiveSearch(RootDirectoryInfo, dbImage.Hash);

            if (result is FileInfo)
            {
                var fileInfo = result as FileInfo;
                var image = Image.FromFile(fileInfo.FullName);
                return ImageToBase64(image);
            }

            return string.Empty;
        }
    }
}
