namespace Leifez.Application.Service.Interfaces
{
    public interface IImageService
    {
        string AddImage(string image);
        string GetImage(string guid);
        bool DeleteImage(string guid);
    }
}
