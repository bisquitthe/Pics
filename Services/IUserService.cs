using System.Threading.Tasks;
using Models;

namespace Services
{
  public interface IUserService
  {
    Task<User> RegisterUser(string login, string password);
  }
}