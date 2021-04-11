using Leifez.Core.PostgreSQL.Models;
using Leifez.Core.PostgreSQL.Models.Enums;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICommonDomain
    {
        DbLike GetLike(string hashId);
        int GetLikes(string entityId, ContentType contentType);
        bool RemoveLike(string hashId);
        bool CreateLike(DbLike like);
    }
}
