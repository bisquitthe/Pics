using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pics.ViewModels;
using Services;

namespace Pics.Controllers
{
  public class ImageController : Controller
  {
    private readonly IImageService imageService;
    public ImageController(IImageService imageService)
    {
      this.imageService = imageService;
    }

    [HttpGet]
    [Route("images")]
    public ObjectResult GetImages(int page)
    {
      var images = this.imageService.GetImages(page);
      return Ok(images);
    }

    [HttpPost]
    [Route("images/new")]
    public OkResult ImportImage(ImageUploadingFileViewModel imageUploadingFile)
    {
      imageUploadingFile.Image.Name = imageUploadingFile.UploadingFile.FileName;
      this.imageService.ImportImage(imageUploadingFile.Image, imageUploadingFile.UploadingFile.OpenReadStream());
      return Ok();
    }

    [HttpPost]
    [Route("images/remove")]
    public OkResult RemoveImage(string id)
    {
      this.imageService.RemoveImage(id);
      return Ok();
    }
  }
}