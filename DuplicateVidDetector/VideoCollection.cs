#nullable enable

using DuplicateVidDetector.Models;
using PW.IO.FileSystemObjects;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateVidDetector
{
  internal class VideoCollection : List<VideoFile>
  {

    public VideoCollection() { }

    public VideoCollection(IEnumerable<VideoFile> items) : base(items) { }

    /// <summary>
    /// Attempts to return a <see cref="VideoFile"/> with the specified file name, ignoring the path and extension.
    /// </summary>
    public ICollection<VideoFile> Find(FileNameWithoutExtension find) => this.Where(x => x.FilePath.NameWithoutExtension == find).ToArray();

    /// <summary>
    /// Attempts to return a <see cref="VideoFile"/> with the specified file name, ignoring the path.
    /// </summary>
    public ICollection<VideoFile> Find(FileName find) => this.Where(x => x.FilePath.Name == find).ToArray();

    /// <summary>
    /// Finds all <see cref="VideoFile"/> instances with the exact size <paramref name="bytes"/>.
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public ICollection<VideoFile> FindBySize(long bytes)
    {

      // HACK - Does not keep track of file-system changes.
      // So remove all missing files from list.
      // This should be done by a watcher
      foreach (var file in  this.Where(x => !x.FilePath.Exists).ToArray()) Remove(file);
          
      return this.Where(x => x.Size == bytes).ToArray();
    }


    #region Grouping Methods

    ///// <summary>
    ///// Predicate: returns true when a group has more than one element.
    ///// </summary>
    //private bool HasMultipleElements<TKey, TElement>(IGrouping<TKey, TElement> group) => group.Count() > 1;

    public IEnumerable<IGrouping<long, VideoFile>> OfSameSize =>
      this.GroupBy(x => x.Size).WhereHasMultipleElements();

    /// <summary>
    /// Single character file names are omitted.
    /// </summary>
    public IEnumerable<IGrouping<FileName, VideoFile>> WithSameName
    {
      get
      {
        static bool Filter(VideoFile item) => item.FilePath.NameWithoutExtension.Value.Length > 1;
        return this.Where(Filter).GroupBy(x => x.FilePath.Name).WhereHasMultipleElements();
      }
    }

    #endregion

  }
}
