using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Models
{
  public class AuthOptions
  {
    public const string Issuer = "MyAuthServer";
    public const string Audience = "http://localhost:50087/";
    const string Key = "a19f393652cb47ae9476553d2d5e3567";
    public const int Lifetime = 5;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
  }
}