using System;
using System.Threading.Tasks;
using DataAccess;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Pics.Controllers
{
  public class UserController : Controller
  {
    private readonly IAuthService authService;
    private readonly IUserService userService;

    [HttpPost("/signin")]
    public async Task<IActionResult> SignIn(string login, string password)
    {
      string jwtString;
      try
      {
        jwtString = await this.authService.GetJwt(login, password);
      }
      catch (UserNotFoundException)
      {
        return BadRequest("Invalid login or password.");
      }

      return Ok(new
      {
        access_token = jwtString,
        username = login
      });
    }

    [HttpPost("/signup")]
    public async Task<IActionResult> SignUp(string login, string password)
    {
      User user = null;
      try
      {
        user = await this.userService.RegisterUser(login, password);
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