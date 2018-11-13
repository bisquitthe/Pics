using System.IO;
using System.Threading.Tasks;
using Models;

namespace DataAccess.FileRepo
{
  public interface IFileRepository<T>
  {
    Task Save(string filename, Stream uploadingFileStream);
    void Remove(string filename);
  }
}