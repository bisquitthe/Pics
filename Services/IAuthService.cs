using System.Threading.Tasks;

namespace Services
{
  public interface IAuthService
  {
    Task<string> GetJwt(string login, string password);
  }
}