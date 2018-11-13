using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Models;

namespace Services
{
  public interface IImageService
  {
    Task<IEnumerable<Image>> GetImages(int page, int pageCapacity);
    Task<bool> ImportImage(ImageCreationInfo imageCreationInfo, Stream uploadingFileStream);
    Task<bool> RemoveImage(string id);
  }
}