using System.Collections.Generic;
using Models;

namespace DTO
{
  public class ImagesWithPagingInfo
  {
    public long TotalCount { get; set; }
    public IEnumerable<Image> Images { get; set; }
    public int CountPerPage { get; set; }
  }
}
