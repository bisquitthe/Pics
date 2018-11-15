using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Pics.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  public class ImageController : Controller
  {
    private readonly IImageService imageService;
    private readonly IUserService userService;    

    public ImageController(IImageService imageService, IUserService userService)
    {
      this.imageService = imageService;
      this.userService = userService;
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages(int page)
    {
      ImagesWithPagingInfo images;
      try
      {
        images = await this.imageService.GetImages(page);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok(images);
    }

    [HttpPost("images/new")]
    public async Task<IActionResult> ImportImage(IFormFile uploadingFile)
    {
      if (uploadingFile == null)
        return BadRequest();

      var imageCreationInfo = new ImageCreationInfo
      {
        Name = uploadingFile.FileName,
        UserId = (await this.userService.GetUserByLogin(User.Identity.Name)).Id
      };
      Image image;
      try
      {
        image = await this.imageService.ImportImage(imageCreationInfo, uploadingFile.OpenReadStream());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message); 
      }

      return Ok(image);
    }

    [HttpDelete("images/remove")]
    public async Task<IActionResult> RemoveImage(string id)
    {
      try
      {
        await this.imageService.RemoveImage(id);
      }
      catch (ImageNotFound ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok();
    }
  }
}