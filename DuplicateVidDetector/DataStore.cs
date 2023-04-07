#nullable enable

using LiteDB;
using PW.Functional;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace DuplicateVidDetector
{
  internal class DataStore
  {
    // NB: There is an issue with this. Types are persisted to a collection named T.Name, rather than T.FullName
    //     As this is not full qualified it could lead to different types being stored in the same collection.
    //     For example, Lib.Something.Stuff would save to the same collection as Lib2.SomthingElse.Stuff

    public DataStore(FilePath dbFilePath, BsonMapper mapper)
    {
      DbFilePath = dbFilePath ?? throw new System.ArgumentNullException(nameof(dbFilePath));
      Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public DataStore(FilePath dbFilePath) : this(dbFilePath, new BsonMapper())
    {
    }

    /// <summary>
    /// <see cref="BsonMapper"/>
    /// </summary>
    public BsonMapper Mapper { get; }

    /// <summary>
    /// Path to the LiteDatabase file.
    /// </summary>
    private FilePath DbFilePath { get; }

    // LiteDatabase will auto-create if missing. But the directory path must exist.
    private LiteRepository Repo => DbFilePath.DirectoryPath.Exists
      ? new LiteRepository(DbFilePath, Mapper)
      : throw new DirectoryNotFoundException("Store directory not found:" + DbFilePath.DirectoryPath);

    /// <summary>
    /// Query all entities of type <typeparamref name="T"/>.
    /// </summary>
    public List<T> All<T>() => Repo.DisposeAfter(repo => repo.Query<T>().ToList());

    /// <summary>
    /// Insert or Update a document based on _id key. Returns true if insert entity or false if update entity
    /// </summary>
    public bool Upsert<T>(T entity) => Repo.DisposeAfter(repo => repo.Upsert(entity));

    public void Drop<T>() => Repo.DisposeAfter(repo => repo.Database.DropCollection(typeof(T).Name));

  }
}
