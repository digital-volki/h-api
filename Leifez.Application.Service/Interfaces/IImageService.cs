using Leifez.Application.Domain.Models;
using System.Collections.Generic;

namespace Leifez.Application.Service.Interfaces
{
    public interface IImageService
    {
        List<string> Add(IEnumerable<string> base64Images, int collectionId);
        List<Image> Get(IEnumerable<string> guids);
        bool Delete(string guid);
        bool Change(Image image);
    }
}
