using PW.IO.FileSystemObjects;

namespace DuplicateVidDetector.Models;

[System.Diagnostics.DebuggerDisplay("{FilePath}")]
internal class VideoFile(FilePath path)
{
  const long lengthNotRead = -1;


  private long length = lengthNotRead;

  /// <summary>
  /// Size of the <see cref="VideoFile"/>, in bytes.
  /// </summary>
  public long Size => length == lengthNotRead ? length = FilePath.ToFileInfo().Length : length;


  /// <summary>
  /// Video file's path.
  /// </summary>
  public FilePath FilePath { get; } = path;

}
