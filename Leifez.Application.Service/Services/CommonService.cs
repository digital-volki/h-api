using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.PostgreSQL.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Leifez.Application.Service.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonDomain _commonDomain;

        public CommonService(
            ICommonDomain commonDomain)
        {
            _commonDomain = commonDomain;
        }
        public static string LikeToMd5(DbLike like)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = JsonSerializer.SerializeToUtf8Bytes((like.UserId, like.EntityId, like.ContentType));

                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public bool Like(string accountId, string entityId, ContentType contentType)
        {
            DbLike like = new DbLike()
            {
                UserId = accountId,
                EntityId = entityId,
                ContentType = contentType
            };
            like.HashId = LikeToMd5(like);

            DbLike currentLike = _commonDomain.GetLike(like.HashId);

            if (currentLike != null)
            {
                return _commonDomain.RemoveLike(like.HashId);
            }

            return _commonDomain.CreateLike(like);
        }
    }
}
