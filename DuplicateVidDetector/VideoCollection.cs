using DuplicateVidDetector.Models;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateVidDetector;

internal partial class VideoCollection : List<VideoFile>
{

  public VideoCollection() { }

  public VideoCollection(IEnumerable<VideoFile> items) : base(items) { }

  /// <summary>
  /// Attempts to return a <see cref="VideoFile"/> with the specified file name, ignoring the path and extension.
  /// </summary>
  public ICollection<VideoFile> Find(FileNameWithoutExtension find) => this.Where(x => x.FilePath.NameWithoutExtension == find).ToArray();

  /// <summary>
  /// Finds video(s) name using wildcard '*' at start, end, or both.
  /// </summary>
  public ICollection<VideoFile> Find(string query)
  {
    const StringComparison compare = StringComparison.OrdinalIgnoreCase;
    static string name(VideoFile vf) => vf.FilePath.NameWithoutExtension.ToString();

    return GetQueryType(query) switch
    {
      QueryTypeOption.Contains => this.Where(x => name(x).Contains(query.Replace("*", ""), compare)).ToArray(),
      QueryTypeOption.StartsWith => this.Where(x => name(x).StartsWith(query.Replace("*", ""), compare)).ToArray(),
      QueryTypeOption.EndsWith => this.Where(x => name(x).EndsWith(query.Replace("*", ""), compare)).ToArray(),
      QueryTypeOption.ExactMatch => (ICollection<VideoFile>)this.Where(x => name(x).Equals(query, compare)).ToArray(),
      _ => throw new NotImplementedException()
    };
  }

  /// <summary>
  /// Determines the query type, based on the position of wildcards.
  /// </summary>
  private static QueryTypeOption GetQueryType(string query)
  {
    return
      query.StartsWith('*') && query.EndsWith('*') ? QueryTypeOption.Contains
      : query.StartsWith('*') ? QueryTypeOption.EndsWith
      : query.EndsWith('*') ? QueryTypeOption.StartsWith
      : QueryTypeOption.ExactMatch;
  }

  /// <summary>
  /// Attempts to return a <see cref="VideoFile"/> with the specified file name, ignoring the path.
  /// </summary>
  public ICollection<VideoFile> Find(FileName find) => this.Where(x => x.FilePath.Name == find).ToArray();

  /// <summary>
  /// Finds all <see cref="VideoFile"/> instances with the exact size <paramref name="bytes"/>.
  /// </summary>
  public ICollection<VideoFile> FindBySize(long bytes)
  {
    // HACK - Does not keep track of file-system changes.
    // So remove all missing files from list.
    // This should be done by a watcher
    foreach (var file in this.Where(x => !x.FilePath.Exists).ToArray()) Remove(file);

    return this.Where(x => x.Size == bytes).ToArray();
  }


  /// <summary>
  /// Returns groupings where there are duplicates, based on size.
  /// </summary>
  public IEnumerable<IGrouping<long, VideoFile>> OfSameSize 
    => this.GroupBy(x => x.Size).WhereHasMultipleElements();

  /// <summary>
  /// Returns groupings where there are duplicates, based on file name, ignoring the file extension.
  /// </summary>
  public IEnumerable<IGrouping<FileNameWithoutExtension, VideoFile>> WithSameName 
    => this.GroupBy(x => x.FilePath.NameWithoutExtension).WhereHasMultipleElements();

}
