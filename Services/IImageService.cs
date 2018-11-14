using System.IO;
using System.Threading.Tasks;
using DTO;
using Models;

namespace Services
{
  public interface IImageService
  {
    Task<ImagesWithPagingInfo> GetImages(int page);
    Task<Image> ImportImage(ImageCreationInfo imageCreationInfo, Stream uploadingFileStream);
    Task<bool> RemoveImage(string id);
  }
}