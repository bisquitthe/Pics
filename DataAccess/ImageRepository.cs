using System.Collections.Generic;
using System.Threading.Tasks;
using Exceptions;
using Models;
using MongoDB.Driver;

namespace DataAccess
{
  public class ImageRepository : IImageRepository
  {
    public readonly IMongoCollection<Image> imageCollection;

    public async Task<ICollection<Image>> GetImages(int skip, int count)
    {
      var images = await this.imageCollection
        .Find(i => true)
        .Skip(skip)
        .Limit(count)
        .ToListAsync();

      return images;
    }

    public async Task<Image> GetImage(string id)
    {
      var image = await imageCollection.Find(i => i.Id == id).SingleOrDefaultAsync();
      return image;
    }

    public async Task AddImage(Image image)
    {
      await this.imageCollection.InsertOneAsync(image);
    }

    public async Task RemoveImage(string id)
    {
      var deleteResult =
        await this.imageCollection.DeleteOneAsync(i => i.Id == id);
      if(deleteResult.DeletedCount == 0)
        throw new ImageNotFound(id);
    }

    public long GetCount()
    {
      return this.imageCollection.CountDocuments(i => true);
    }

    public ImageRepository(IMongoCollection<Image> imageCollection)
    {
      this.imageCollection = imageCollection;
      this.imageCollection.Indexes.CreateOne(new CreateIndexModel<Image>(
        Builders<Image>.IndexKeys.Ascending(i => i.Name), new CreateIndexOptions() {Unique = true}));
    }
  }
}