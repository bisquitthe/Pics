using System;
using System.Collections.Generic;
using System.IO;
using DataAccess;
using DataAccess.FileRepo;
using Models;

namespace Services
{
  public class ImageService : IImageService
  {
    private readonly IImageRepository dbImageRepository;
    private readonly IFileRepository imageFileRepository;

    public Image GetImage(string id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Image> GetImages(int page)
    {
      throw new System.NotImplementedException();
    }

    public void ImportImage(Image image, Stream uploadingFileStream)
    {
      this.dbImageRepository.AddImage(image);
      this.imageFileRepository.Save(image.Name, uploadingFileStream);
    }

    public void RemoveImage(string id)
    {
      throw new System.NotImplementedException();
    }

    public ImageService(IImageRepository dbImageRepository, IFileRepository imageFileRepository)
    {
      this.dbImageRepository = dbImageRepository;
      this.imageFileRepository = imageFileRepository;
    }
  }
}