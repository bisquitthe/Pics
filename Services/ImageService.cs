using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess;
using DataAccess.FileRepo;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Services
{
  public class ImageService : IImageService
  {
    private readonly IImageRepository dbImageRepository;
    private readonly IFileRepository<Image> imageFileRepository;

    public async Task<IEnumerable<Image>> GetImages(int page, int pageCapacity)
    {
      return await this.dbImageRepository.GetImages(pageCapacity * page, pageCapacity);
    }

    public async Task<bool> ImportImage(ImageCreationInfo imageCreationInfo, Stream uploadingFileStream)
    {
      var image = new Image
      {
        Name = imageCreationInfo.Name,
        CreationTime = DateTime.Now,
        UserId = ObjectId.Parse(imageCreationInfo.UserId)
      };

      //Needs transactions.
      await this.dbImageRepository.AddImage(image);
      await this.imageFileRepository.Save(image.Name, uploadingFileStream);

      return true;
    }

    public async Task<bool> RemoveImage(string id)
    {
      var image = await this.dbImageRepository.GetImage(id);
      if (image == null)
        return false;

      //Need transactions.
      await this.dbImageRepository.RemoveImage(id);
      this.imageFileRepository.Remove(image.Name);

      return true;
    }

    public ImageService(IImageRepository dbImageRepository, IFileRepository<Image> imageFileRepository)
    {
      this.dbImageRepository = dbImageRepository;
      this.imageFileRepository = imageFileRepository;
    }
  }
}