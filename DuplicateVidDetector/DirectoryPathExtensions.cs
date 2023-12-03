using DuplicateVidDetector.Models;
using PW.IO.FileSystemObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateVidDetector;

internal static class DirectoryPathExtensions
{
  public static IEnumerable<VideoFile> EnumerateVideoFiles(this DirectoryPath root) =>
    FileSystem.EnumerateFiles(root, "*.*", SearchOption.AllDirectories)
    .Where(x => x.Extension.IsVideo())
    .Select(x => new VideoFile(x));

}
