using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.Extensions.Logging;
using System;
using DrawingImage = System.Drawing.Image;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Leifez.Common.Mapping;
using Leifez.Application.Domain.Models;

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

        private DrawingImage Base64ToImage(string base64string)
        {
            var base64stringCut = base64string.Substring(base64string.IndexOf(',') + 1);
            byte[] imageBytes = Convert.FromBase64String(base64stringCut);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                var image = DrawingImage.FromStream(ms, true);
                return image;
            }
        }

        private string ImageToBase64(DrawingImage image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private string ImageToMd5(DrawingImage image)
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
                _logger.LogInformation($"Files directory create. Path: {currentDirectoryInfo.FullName}");
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
                    f.MoveTo($@"{localDirectory.FullName}{Path.DirectorySeparatorChar}{f.Name}");
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

        public List<string> Add(IEnumerable<string> base64Images)
        {
            var result = new List<string>();
            var imagesToCreate = new List<DbImage>();
            foreach (var base64Image in base64Images)
            {
                _logger.LogInformation("Converting base64 to image");
                try
                {
                    DrawingImage image = Base64ToImage(base64Image);
                    DbImage imageModel = new DbImage()
                    {
                        Hash = ImageToMd5(image)
                    };
    
                    _logger.LogInformation("Converting base64 to image end");
                    _logger.LogInformation("Search and Create image");

                    try
                    {
                        var guid = _imageDomain.FindByHash(imageModel.Hash);
                        if (!string.IsNullOrEmpty(guid))
                        {
                            imageModel.Guid = guid;
                            result.Add(imageModel.Guid);
                            continue;
                        }

                        _logger.LogInformation("RecursiveSearch image");
                        var findImage = RecursiveSearch(RootDirectoryInfo, imageModel.Hash);
                        _logger.LogInformation("RecursiveSearch image end");
                        if (findImage is DirectoryInfo)
                        {
                            var directoryInfo = findImage as DirectoryInfo;
                            var extension = image.RawFormat.ToString().ToLower();
                            var path = $@"{directoryInfo.FullName}{Path.DirectorySeparatorChar}{imageModel.Hash}.{extension}";
                            _logger.LogInformation($"Image create in directory. Path: {path}");
                            image.Save(path);
                        }

                        imageModel.Guid = Guid.NewGuid().ToString();
                        imageModel.CreatedAt = DateTime.UtcNow;
                        imageModel.UpdatedAt = DateTime.UtcNow;
                        imagesToCreate.Add(imageModel);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, e.Message);
                        return new List<string>();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }

            if (_imageDomain.Add(imagesToCreate))
            {
                result.AddRange(imagesToCreate.Select(i => i.Guid));
            }

            return result;
        }

        public bool Delete(string guid)
        {
            throw new System.NotImplementedException();
        }

        public List<string> Get(IEnumerable<string> guids)
        {
            var result = new List<string>();
            var dbImages = _imageDomain.Get(guids);
            foreach (var dbImage in dbImages)
            {
                var findImage = RecursiveSearch(RootDirectoryInfo, dbImage.Hash);
                if (findImage is FileInfo)
                {
                    var fileInfo = findImage as FileInfo;
                    var image = DrawingImage.FromFile(fileInfo.FullName);
                    result.Add(ImageToBase64(image));
                }
            }

            return result;
        }

        public bool Change(Image image)
        {
            DbImage dbImage = _imageDomain.Get(image.Guid);
            dbImage.Tags.AddRange(image.Tags.MapToList<Tag, DbTag>(_mapper));
            image.UpdatedAt = DateTime.UtcNow;

            return _imageDomain.Update(dbImage);
        }
    }
}
