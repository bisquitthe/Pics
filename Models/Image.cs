﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
  public class Image
  {
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public string ShortTitle { get; set; }
    public string Name { get; set; }
  }

  public class ImageCreationInfo
  {
    public string UserId { get; set; }
    public string Name { get; set; }
  }
}
