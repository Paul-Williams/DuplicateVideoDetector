using LiteDB;
using PW.IO.FileSystemObjects;

namespace DuplicateVidDetector;

internal class DataMapper : BsonMapper
{
  public DataMapper()
  {
    RegisterType(filePath => filePath.ToString(), bsonValue => new FilePath(bsonValue));
  }
}
