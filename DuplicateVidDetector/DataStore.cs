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
  private FilePath DbFilePath { get; } = dbFilePath ?? throw new ArgumentNullException(nameof(dbFilePath));

  // LiteDatabase will auto-create if missing. But the directory path must exist.
  private LiteRepository Repo => DbFilePath.DirectoryPath.Exists
    ? new LiteRepository(DbFilePath.ToString(), Mapper)
    : throw new DirectoryNotFoundException("Store directory not found:" + DbFilePath.DirectoryPath);

  /// <summary
  /// Query all entities of type <typeparamref name="T"/>.
  /// </summary>
  public List<T> All<T>() => Repo.Using(repo => repo.Query<T>().ToList()) ?? [];

  /// <summary>
  /// Insert or Update a document based on _id key. Returns true if insert entity or false if update entity
  /// </summary>
  public bool Upsert<T>(T entity) => Repo.Using(repo => repo.Upsert(entity));

  /// <summary>
  /// Removes a collection from the database based on the type provided. The collection name corresponds to the type <see cref="T"/>
  /// </summary>
  /// <typeparam name="T">Specifies the type whose collection will be dropped from the database.</typeparam>
  public void Drop<T>() => Repo.Using(repo => repo.Database.DropCollection(typeof(T).Name));

}
