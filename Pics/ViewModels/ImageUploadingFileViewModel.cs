using Microsoft.AspNetCore.Http;
using Models;

namespace Pics.ViewModels
{
  public class ImageUploadingFileViewModel
  {
    public IFormFile UploadingFile { get; set; }
    public Image Image { get; set; }
  }
}