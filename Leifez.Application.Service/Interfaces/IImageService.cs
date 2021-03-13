using System.Collections.Generic;

namespace Leifez.Application.Service.Interfaces
{
    public interface IImageService
    {
        List<string> AddImages(IEnumerable<string> base64Images);
        List<string> GetImages(IEnumerable<string> guids);
        bool DeleteImage(string guid);
    }
}
