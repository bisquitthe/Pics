using System.Threading.Tasks;
using Models;

namespace DataAccess
{
  public interface IUserRepository
  {
    Task<User> GetByLoginAndPasswordHash(string login, string passwordHash);
    Task<User> AddUser(string login, string passwordHash);
  }
}