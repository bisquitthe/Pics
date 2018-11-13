using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
  public static class HashHelper
  {
    public static string GetHash(string str)
    {
      var strBytes = Encoding.ASCII.GetBytes(str);
      var sha1 = new SHA1CryptoServiceProvider();
      var hashedStr = sha1.ComputeHash(strBytes);
      return Encoding.ASCII.GetString(hashedStr);
    }
  }
}
