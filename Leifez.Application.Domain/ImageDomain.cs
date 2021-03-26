using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Leifez.Application.Domain.Interfaces;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class ImageDomain : IImageDomain
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public ImageDomain(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public bool Add(IEnumerable<DbImage> imageModels)
        {
            if (imageModels == null || !imageModels.Any())
            {
                return false;
            }

            var dbImages = _dataContext.InsertMany(imageModels);

            if (dbImages == null || !dbImages.Any())
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }

        public bool Delete(string guid)
        {
            var dbImage = _dataContext.GetQueryable<DbImage>().Where(x => x.Guid == guid).FirstOrDefault();
            var deleteImage = _dataContext.Delete(dbImage);

            return _dataContext.Save() != 0;
        }

        public List<DbImage> Get(IEnumerable<string> guids)
        {
            return _dataContext.GetQueryable<DbImage>()
                .Include(i => i.Tags).ToList()
                .Where(x => guids.Contains(x.Guid)).ToList();
        }
        public DbImage Get(string guid)
        {
            return _dataContext.GetQueryable<DbImage>()
                .Include(i => i.Tags).ToList()
                .Where(x => x.Guid == guid).FirstOrDefault();
        }

        public string FindByHash(string hash)
        {
            return _dataContext.GetQueryable<DbImage>().Where(x => x.Hash == hash).FirstOrDefault()?.Guid;
        }

        public bool Update(DbImage dbImage)
        {
            if (dbImage == null)
            {
                return false;
            }

            var resImage = _dataContext.Update(dbImage);
            if (resImage == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }
    }
}
