#nullable enable

using DuplicateVidDetector.Models;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Text;

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
    /// Finds a file name using wildcard '*' at start, end, or both.
    /// </summary>
    public ICollection<VideoFile> Find(string query)
    {
      const StringComparison compare = StringComparison.OrdinalIgnoreCase;
      static string name(VideoFile vf) => vf.FilePath.NameWithoutExtension.ToString();

      return GetQueryType(query) switch
      {
        QueryType.Contains => this.Where(x => name(x).Contains(query.Replace("*", ""), compare)).ToArray(),
        QueryType.StartsWith => this.Where(x => name(x).StartsWith(query.Replace("*", ""), compare)).ToArray(),
        QueryType.EndsWith => this.Where(x => name(x).EndsWith(query.Replace("*", ""), compare)).ToArray(),
        QueryType.ExactMatch => (ICollection<VideoFile>)this.Where(x => name(x).Equals(query, compare)).ToArray(),
        _ => throw new NotImplementedException()
      };


      //return

      //  // Wildcard both ends = contains query
      //  query.StartsWith('*') && query.EndsWith('*')
      //  ? this.Where(x => name(x).Contains(query.Replace("*", ""), StringComparison.OrdinalIgnoreCase)).ToArray()

      //  // Wildcard at start = ends-with query
      //  : query.StartsWith('*')
      //  ? this.Where(x => name(x).EndsWith(query.Replace("*", ""), StringComparison.OrdinalIgnoreCase)).ToArray()

      //  // Wildcard at end = starts-with query
      //  : query.EndsWith('*')
      //  ? this.Where(x => name(x).StartsWith(query.Replace("*", ""), StringComparison.OrdinalIgnoreCase)).ToArray()

      //  : (ICollection<VideoFile>)this.Where(x => name(x) == query).ToArray();

    }

    /// <summary>
    /// Supported query types.
    /// </summary>
    private enum QueryType { Contains, StartsWith, EndsWith, ExactMatch };


    /// <summary>
    /// Determines the query type, based on the position of wildcards.
    /// </summary>
    private static QueryType GetQueryType(string query)
    {
      return
        query.StartsWith('*') && query.EndsWith('*') ? QueryType.Contains
        : query.StartsWith('*') ? QueryType.EndsWith
        : query.EndsWith('*') ? QueryType.StartsWith
        : QueryType.ExactMatch;
    }




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
      foreach (var file in this.Where(x => !x.FilePath.Exists).ToArray()) Remove(file);

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
