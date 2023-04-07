//using PW.Extensions;
//using PW.IO.FileSystemObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DuplicateVidDetector
//{
//  internal partial class IgnoredGroups
//  {
//    /// <summary>
//    /// ctor
//    /// </summary>
//    public IgnoredGroups(FilePath dbPath)
//    {
//      if (dbPath is null) throw new ArgumentNullException(nameof(dbPath));
//      using var db = new LiteDB.LiteRepository(dbPath.Value);
//      //ignoredGroups = db.Query<IgnoredGroup>().ToList();
//    }

//    public int Add(FilePath[] paths) => ignoredGroups.Add(new IgnoredGroup() { Paths = paths });


//    public bool ShouldIgnore(FilePath[] paths) => ignoredGroups.Any(x => x.Match(paths));


//  }


//  internal class LiteStore<T> : IDisposable
//  {
//    private LiteDB.LiteRepository db {get;}
   
//    public LiteStore(FilePath file)
//    {
//      db = new LiteDB.LiteRepository(file.Value);
//    }

//    public void Dispose() => db?.Dispose();



//  }


//}
