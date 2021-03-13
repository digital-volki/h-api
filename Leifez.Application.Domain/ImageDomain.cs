using AutoMapper;
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

        public bool Add(DbImage imageModel)
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

        public bool Delete(string guid)
        {
            var dbImage = _dataContext.GetQueryable<DbImage>().Where(x => x.Guid == guid).FirstOrDefault();
            var deleteImage = _dataContext.Delete(dbImage);

            return _dataContext.Save() != 0;
        }

        public List<DbImage> Get(IEnumerable<string> guids)
        {
            return _dataContext.GetQueryable<DbImage>().Where(x => guids.Contains(x.Guid)).ToList();
        }

        public string FindByHash(string hash)
        {
            return _dataContext.GetQueryable<DbImage>().Where(x => x.Hash == hash).FirstOrDefault().Guid;
        }
    }
}
