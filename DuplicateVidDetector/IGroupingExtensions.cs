using System;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateVidDetector;

internal static class IGroupingExtensions
{
  /// <summary>
  /// Where clause: returns those groups having more than one element.
  /// </summary>
  public static IEnumerable<IGrouping<TKey, TElement>> WhereHasMultipleElements<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> group)
    => group.Where(g => g.Count() > 1);


  public static IEnumerable<IGrouping<TKey, TSource>> FindDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) 
    => source.GroupBy(keySelector).WhereHasMultipleElements();

  public static IDictionary<TKey, int> FindDuplicateCounts<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull 
    => source.FindDuplicates(keySelector).ToDictionary(x => x.Key, y => y.Count());


}
