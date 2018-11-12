using MongoDB.Bson;

namespace Models
{
  public class User
  {
    public ObjectId Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
  }
}
