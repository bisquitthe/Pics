using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess;
using DataAccess.FileRepo;
using DTO;
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
    private const int PageCapacity = 10;
    public async Task<ImagesWithPagingInfo> GetImages(int page)
    {
      if (page <= 0)
        throw new ArgumentException("Page cannot be equal or less than 0.");

      var images = await this.dbImageRepository.GetImages(skip: PageCapacity * (page - 1), count: PageCapacity);
      foreach (var image in images)
      {
        image.Base64 = await GetImageBase64(image);
      }
      var imagesWithPagingInfo = new ImagesWithPagingInfo
      {
        CountPerPage = PageCapacity,
        Images = images,
        TotalCount = this.dbImageRepository.GetCount()
      };
      return imagesWithPagingInfo;
    }

    public async Task<Image> ImportImage(ImageCreationInfo imageCreationInfo, Stream uploadingFileStream)
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

      image.Base64 = await GetImageBase64(image);

      return image;
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

    private async Task<string> GetImageBase64(Image image)
    {
      var imageBytes = await File.ReadAllBytesAsync(image.Filename);
      var base64 = Convert.ToBase64String(imageBytes);
      return base64;
    }

    public ImageService(IImageRepository dbImageRepository, IFileRepository<Image> imageFileRepository)
    {
      this.dbImageRepository = dbImageRepository;
      this.imageFileRepository = imageFileRepository;
    }
  }
}