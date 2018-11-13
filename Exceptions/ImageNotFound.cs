using System;

namespace Exceptions
{
  public class ImageNotFound : Exception
  {
    public ImageNotFound(string imageId) : base($"Image not found. Id: {imageId}") { }
  }
}