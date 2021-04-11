using Leifez.Application.Domain.Interfaces;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class CommonDomain : ICommonDomain
    {
        private readonly IDataContext _dataContext;

        public CommonDomain(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateLike(DbLike like)
        {
            if (like == null)
            {
                return false;
            }

            DbLike insertLike = _dataContext.Insert(like);
            if (insertLike == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }

        public DbLike GetLike(string hashId)
        {
            return _dataContext.GetQueryable<DbLike>().Where(l => l.HashId == hashId).FirstOrDefault();
        }

        public int GetLikes(string entityId, ContentType contentType)
        {
            return _dataContext.GetQueryable<DbLike>().Where(l => l.EntityId == entityId && l.ContentType == contentType).Count();
        }

        public bool RemoveLike(string hashId)
        {
            DbLike removeLike = _dataContext.Delete(GetLike(hashId));
            if (removeLike == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }
    }
}
