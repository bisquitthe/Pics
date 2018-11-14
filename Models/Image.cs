using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
  public class Image
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public string Name { get; set; }
    public string Filename { get; set; }
  }

  public class ImageCreationInfo
  {
    public string UserId { get; set; }
    public string Name { get; set; }
  }
}
