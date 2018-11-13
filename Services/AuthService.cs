using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess;
using Exceptions;
using Microsoft.IdentityModel.Tokens;
using Models;
using Utils;

namespace Services
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository userRepository;

    public async Task<string> GetJwt(string login, string password)
    {
      var identity = await GetIdentity(login, password);
      if (identity == null)
      {
        throw new UserNotFoundException();
      }

      var now = DateTime.UtcNow;
      var jwt = new JwtSecurityToken(
        issuer: AuthOptions.Issuer,
        audience: AuthOptions.Audience,
        notBefore: now,
        claims: identity.Claims,
        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
          SecurityAlgorithms.HmacSha256));

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      return encodedJwt;
    }

    private async Task<ClaimsIdentity> GetIdentity(string login, string password)
    {
      var passwordHash = HashHelper.GetHash(password);
      var user = await this.userRepository.GetByLoginAndPasswordHash(login, passwordHash);
      if (user == null)
        return null;

      var claims = new List<Claim>
      {
        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
      };
      ClaimsIdentity claimsIdentity =
        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
          ClaimsIdentity.DefaultRoleClaimType);

      return claimsIdentity;
    }

    public AuthService(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }
  }
}
