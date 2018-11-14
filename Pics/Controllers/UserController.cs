using System;
using System.Threading.Tasks;
using DataAccess;
using Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Pics.ViewModels;
using Services;

namespace Pics.Controllers
{
  [ApiController]
  [DisableCors]
  public class UserController : Controller
  {
    private readonly IAuthService authService;
    private readonly IUserService userService;

    [HttpPost("[controller]/signin")]
    public async Task<IActionResult> SignIn(Credentials credentials)
    {
      string jwtString;
      try
      {
        jwtString = await this.authService.GetJwt(credentials.Login, credentials.Password);
      }
      catch (UserNotFoundException)
      {
        return BadRequest("Invalid login or password.");
      }

      var tokenViewModel = new TokenResponseViewModel()
      {
        login = credentials.Login,
        access_token = jwtString
      };

      return Ok(tokenViewModel);
    }

    [HttpPost("[controller]/signup")]
    public async Task<IActionResult> SignUp(Credentials credentials)
    {
      User user = null;
      try
      {
        user = await this.userService.RegisterUser(credentials.Login, credentials.Password);
      }
      catch (UserExistsException)
      {
        return BadRequest("User exists");
      }
      catch (ArgumentNullException)
      {
        return BadRequest("Login and password cannot be empty");
      }

      return Ok(user);
    }

    public UserController(IAuthService authService, IUserService userService)
    {
      this.authService = authService;
      this.userService = userService;
    }
  }
}