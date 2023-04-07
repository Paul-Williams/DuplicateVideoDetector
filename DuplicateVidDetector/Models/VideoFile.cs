#nullable enable

using PW.IO.FileSystemObjects;
using System.IO;

namespace DuplicateVidDetector.Models
{
  [System.Diagnostics.DebuggerDisplay("{FilePath}")]
  internal class VideoFile
  {
    public VideoFile(FilePath path)
    {
      FilePath = path;
    }

    // HACK - Delay reading property, as file may not have finished downloading
    public long Size => FilePath.ToFileInfo().Length;

    public FilePath FilePath { get; }

  }
}
