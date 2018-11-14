using Microsoft.AspNetCore.Mvc;

namespace Pics.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}