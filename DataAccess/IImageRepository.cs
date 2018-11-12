using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataAccess
{
  public interface IImageRepository
  {
    Task<ICollection<Image>> GetImages(int page);
    Task<Image> GetImage(string id);
    Task AddImage(Image image);
    Task RemoveImage(string image);
  }
}