using Leifez.Core.PostgreSQL.Models;

namespace Leifez.Application.Domain.Interfaces
{
    public interface IImageDomain
    {
        bool AddImage(DbImage imageModel);
        DbImage GetImage(string guid);
        bool DeleteImage(string guid);
    }
}
