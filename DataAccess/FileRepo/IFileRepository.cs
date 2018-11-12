using System.IO;
using System.Threading.Tasks;
using Models;

namespace DataAccess.FileRepo
{
  public interface IFileRepository
  {
    Task Save(string filename, Stream uploadingFileStream);
    Task<Image> Load(string filename);
    bool Exists(string filename);
  }
}