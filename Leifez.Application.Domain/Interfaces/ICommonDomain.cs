using Leifez.Core.PostgreSQL.Models;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICommonDomain
    {
        DbLike GetLike(string hashId);
        bool RemoveLike(string hashId);
        bool CreateLike(DbLike like);
    }
}
