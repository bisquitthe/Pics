using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DataAccess
{
  public class ImageRepository : IImageRepository
  {
    public readonly IMongoCollection<Image> imageCollection;
    public ImageRepository(IMongoCollection<Image> imageCollection)
    {
      this.imageCollection = imageCollection;
    }

    public async Task<ICollection<Image>> GetImages(int page)
    {
      var images = await this.imageCollection.Find(i => true).ToListAsync();
      return images;
    }

    public async Task<Image> GetImage(string id)
    {
      var image = await imageCollection.Find(i => i.Id.ToString() == id).SingleAsync();
      return image;
    }

    public async Task AddImage(Image image)
    {
      await this.imageCollection.InsertOneAsync(image);
    }

    public Task RemoveImage(string image)
    {
      throw new System.NotImplementedException();
    }
  }
}