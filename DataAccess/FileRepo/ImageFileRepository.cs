using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.FileRepo;
using Models;

namespace DataAccess
{
  public class ImageFileRepository : IFileRepository<Image>
  {
    private readonly string directoryName;
    public DirectoryInfo DirectoryInfo { get; private set; }

    public async Task Save(string filename, Stream uploadingFileStream)
    {
      var combinedFilename = Path.Combine(directoryName, filename);
      using (uploadingFileStream)
      using (var imageFile = new FileStream(combinedFilename, FileMode.CreateNew))
      {
        await uploadingFileStream.CopyToAsync(imageFile);
      }
    }

    public void Remove(string filename)
    {
      var fileInfo = this.GetFileInfo(Path.Combine(this.directoryName, filename));
      if (!fileInfo.Exists)
        return;

      fileInfo.Delete();
    }

    private FileInfo GetFileInfo(string filename)
    {
      var fileInfo = new FileInfo(filename);
      return fileInfo;
    }

    public ImageFileRepository(string rootPath)
    {
      this.directoryName = Path.Combine(rootPath, "user_images_files");
      this.DirectoryInfo = new DirectoryInfo(this.directoryName);
    }
  }
}
