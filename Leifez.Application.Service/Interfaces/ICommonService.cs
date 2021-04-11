using Leifez.Core.PostgreSQL.Models.Enums;

namespace Leifez.Application.Service.Interfaces
{
    public interface ICommonService
    {
        bool Like(string accountId, string entityId, ContentType contentType);
    }
}
