using PW.Extensions;
using PW.IO.FileSystemObjects;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateVidDetector.Models;

internal class IgnoredGroup(IEnumerable<FilePath> files)
{
  public int Id { get; set; }

  public FilePath[]? Files { get; set; } = files.ToArray();

  /// <summary>
  /// Returns true if <paramref name="set"/> contains the same <see cref="FilePath"/>s as this instance. 
  /// Returns false if either <see cref="FilePath"/> array is null or they contain a different number of elements.
  /// </summary>
  public bool Matches(FilePath[]? set) =>
    Files is not null
    && set is not null
    && Files.Length == set.Length
    && Files.Except(set).None();

}
