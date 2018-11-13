using System;
using System.Threading.Tasks;
using Exceptions;
using Models;
using MongoDB.Driver;

namespace DataAccess
{
  public class UserRepository : IUserRepository
  {
    private readonly IMongoCollection<User> usersCollection;

    public async Task<User> GetByLoginAndPasswordHash(string login, string passwordHash)
    {
      var user = await this.usersCollection.Find(u => u.Login == login && u.PasswordHash == passwordHash).SingleOrDefaultAsync();

      return user;
    }

    public async Task<User> AddUser(string login, string passwordHash)
    {
      var newUser = new User()
      {
        Login = login,
        PasswordHash = passwordHash
      };

      await this.usersCollection.InsertOneAsync(newUser);

      return newUser;
    }

    public async Task<User> GetUserByLogin(string login)
    {
      var user = await this.usersCollection.Find(u => u.Login == login).SingleOrDefaultAsync();
      return user;
    }

    public UserRepository(IMongoCollection<User> usersCollection)
    {
      this.usersCollection = usersCollection;
      this.usersCollection.Indexes.CreateOne(
        new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u => u.Login),
          new CreateIndexOptions() {Unique = true}));
    }
  }
}