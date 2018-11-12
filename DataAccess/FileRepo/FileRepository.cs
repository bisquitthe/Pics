using Microsoft.AspNetCore.Hosting;

namespace DataAccess.FileRepo
{
  public abstract class FileRepository
  {
  
    public abstract void Save(string filename);
    public abstract void Load(string filename);
    public abstract bool Exists(string filename);
  }
}