using LiteDB;
using PW.Functional;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace DuplicateVidDetector;

internal class DataStore(FilePath dbFilePath, BsonMapper mapper)
{
  public DataStore(FilePath dbFilePath) : this(dbFilePath, new BsonMapper()) { }

  /// <summary>
  /// <see cref="BsonMapper"/>
  /// </summary>
  public BsonMapper Mapper { get; } = mapper ?? throw new ArgumentNullException(nameof(mapper));

  /// <summary>
  /// Path to the LiteDatabase file.
  /// </summary>
  private FilePath DbFilePath { get; } = dbFilePath ?? throw new System.ArgumentNullException(nameof(dbFilePath));

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
