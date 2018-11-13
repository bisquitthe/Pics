using System;
using System.Threading.Tasks;
using DataAccess;
using Exceptions;
using Models;
using Utils;

namespace Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository userRepository;
    public async Task<User> RegisterUser(string login, string password)
    {
      if(string.IsNullOrWhiteSpace(login))
        throw new ArgumentNullException(nameof(login));

      if (string.IsNullOrWhiteSpace(password))
        throw new ArgumentNullException(nameof(password));

      User user;
      var passwordHash = HashHelper.GetHash(password);
      try
      {
        user = await this.userRepository.AddUser(login, passwordHash);
      }
      catch (Exception)
      {
        throw new UserExistsException();
      }

      return user;
    }

    public async Task<User> GetUserByLogin(string login)
    {
      return await this.userRepository.GetUserByLogin(login);
    }

    public UserService(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }
  }
}