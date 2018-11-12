using System.IO;
using System.Threading.Tasks;
using DataAccess.FileRepo;
using Models;

namespace DataAccess
{
  class ImageFileRepository : IFileRepository
  {
    private readonly string directoryName;

    public ImageFileRepository(string rootPath)
    {
      this.directoryName = Path.Combine(rootPath, "user_images_files");
    }

    public async Task Save(string filename, Stream uploadingFileStream)
    {
      var combinedFilename = Path.Combine(directoryName, filename);
      using (uploadingFileStream)
      using (var imageFile = new FileStream(combinedFilename, FileMode.CreateNew))
      {
        await uploadingFileStream.CopyToAsync(imageFile);
      }
    }

    public Task<Image> Load(string filename)
    {
      throw new System.NotImplementedException();
    }

    public bool Exists(string filename)
    {
      throw new System.NotImplementedException();
    }
  }
}
