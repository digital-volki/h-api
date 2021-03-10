using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
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

        public bool AddImage(DbImage imageModel)
        {
            if (imageModel == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(imageModel.Guid) || string.IsNullOrEmpty(imageModel.Hash))
            {
                return false;
            }

            var dbImage = _dataContext.Insert(imageModel);

            if (dbImage == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }

        public bool DeleteImage(string guid)
        {
            var dbImage = _dataContext.GetQueryable<DbImage>().Where(x => x.Guid == guid).FirstOrDefault();
            var deleteImage = _dataContext.Delete(dbImage);

            return _dataContext.Save() != 0;
        }

        public DbImage GetImage(string guid)
        {
            return _dataContext.GetQueryable<DbImage>().Where(x => x.Guid == guid).FirstOrDefault();
        }
    }
}
