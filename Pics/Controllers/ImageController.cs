using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public IActionResult GetImages(int page)
    {
      var images = this.imageService.GetImages(page, PageCapacity);
      return Ok(images);
    }

    [HttpPost]
    [Route("images/new")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ImportImage(IFormFile uploadingFile)
    {
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
      catch (Exception ex)
      {
        return BadRequest(ex.Message); 
      }

      if (!imported)
        return BadRequest();

      return Ok();
    }

    [HttpPost]
    [Route("images/remove")]
    public IActionResult RemoveImage(string id)
    {
      this.imageService.RemoveImage(id);
      return Ok();
    }
  }
}