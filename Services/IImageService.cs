using System.Collections.Generic;
using System.IO;
using Models;

namespace Services
{
  public interface IImageService
  {
    Image GetImage(string id);
    IEnumerable<Image> GetImages(int page);
    void ImportImage(Image image, Stream uploadingFileStream);
    void RemoveImage(string id);
  }
}