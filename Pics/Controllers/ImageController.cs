using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Bson;
using Services;

namespace Pics.Controllers
{
  public class ImageController : Controller
  {
    private readonly IImageService imageService;
    private const int PageCapacity = 10;

    public ImageController(IImageService imageService)
    {
      this.imageService = imageService;
    }

    [HttpGet]
    [Route("images")]
    public ObjectResult GetImages(int page)
    {
      var images = this.imageService.GetImages(page, PageCapacity);
      return Ok(images);
    }

    [HttpPost]
    [Route("images/new")]
    public async Task<ActionResult> ImportImage()
    {
      var uploadingFile = Request.Form.Files.FirstOrDefault();
      if (uploadingFile == null)
        return BadRequest();
      var imported = false;
      var imageCreationInfo = new ImageCreationInfo
      {
        Name = uploadingFile.FileName,
        UserId = ObjectId.GenerateNewId().ToString()
      };
      try
      {
        imported = await this.imageService.ImportImage(imageCreationInfo, uploadingFile.OpenReadStream());
      }
      catch (Exception)
      {
        return BadRequest(); 
      }

      if (!imported)
        return BadRequest();

      return Ok();
    }

    [HttpPost]
    [Route("images/remove")]
    public ActionResult RemoveImage(string id)
    {
      this.imageService.RemoveImage(id);
      return Ok();
    }
  }
}