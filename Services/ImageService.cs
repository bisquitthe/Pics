using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess;
using DataAccess.FileRepo;
using Exceptions;
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
      if (page <= 0)
        throw new ArgumentException("Page cannot be equal or less than 0.");

      return await this.dbImageRepository.GetImages(skip: pageCapacity * (page - 1), count: pageCapacity);
    }

    public async Task<bool> ImportImage(ImageCreationInfo imageCreationInfo, Stream uploadingFileStream)
    {
      var image = new Image
      {
        Name = imageCreationInfo.Name,
        CreationTime = DateTime.Now,
        UserId = imageCreationInfo.UserId,
        Filename = Path.Combine(this.imageFileRepository.DirectoryInfo.FullName, imageCreationInfo.Name)
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
        throw new ImageNotFound(id);

      try
      {
        //Need transactions.
        await this.dbImageRepository.RemoveImage(id);
        this.imageFileRepository.Remove(image.Name);
      }
      catch (ImageNotFound)
      {
        //log
        throw;
      }

      return true;
    }

    public long GetPagesCount(int pageCapacity)
    {
      var imagesCount = this.dbImageRepository.GetCount();
      return (imagesCount % pageCapacity) == 0 ?
        (imagesCount / pageCapacity) :
        (imagesCount / pageCapacity + 1);
    }

    public ImageService(IImageRepository dbImageRepository, IFileRepository<Image> imageFileRepository)
    {
      this.dbImageRepository = dbImageRepository;
      this.imageFileRepository = imageFileRepository;
    }
  }
}