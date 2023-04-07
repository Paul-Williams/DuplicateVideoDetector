#nullable enable

using LiteDB;
using Prism.Events;
using PW.IO.FileSystemObjects;
using PW.WinForms;
using System;
using System.Windows.Forms;
using Unity;
using PW.ValueObjects;

namespace DuplicateVidDetector
{
  static class Program
  {
    // HARDCODE: Directory path
    public static DirectoryPath LibraryDirectory { get; } = (DirectoryPath)@"P:\Porn\";

    // HARDCODE: Directory path
    public static DirectoryPath DownloadsDirectory { get; } = (DirectoryPath)@"P:\Downloads\";

    // HARDCODE: File path
    public static FilePath DbPath { get; } = (FilePath)@"P:\DB\DuplicateVidDetector.litedb";

    //public static Prism.Events.EventAggregator Events { get; } = new Prism.Events.EventAggregator();

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
#if !DEBUG
     PW.LaunchPad.RegistrationManager.Register("Duplicate Video Detector", Application.ExecutablePath);
#endif

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      IUnityContainer container = new UnityContainer();
      container.PerformRegistrations();

      if (LibraryDirectory.Exists) Application.Run(container.Resolve<MainForm>()); // Required to set Init property
      else MsgBox.ShowError("Unable to continue. Library path does not exist: " + LibraryDirectory);
    }

    private static void PerformRegistrations(this IUnityContainer container)
    {
      container.RegisterType<MainForm>();
      container.RegisterInstance<IEventAggregator>(new EventAggregator());
      container.RegisterInstance(new DataStore(DbPath, new DataMapper())); //LiteDbMapper));
    }

    //private static BsonMapper LiteDbMapper
    //{
    //  get
    //  {
    //    var mapper = new BsonMapper();

    //    // Map FilePath <-> string for persistence.
    //    mapper.RegisterType(filePath => filePath.ToString(), bsonValue => new FilePath(bsonValue));
    //    return mapper;
    //  }
    //}
  }


  internal class LibraryDirectoryPath : ValueOf<string, LibraryDirectoryPath> { }

  internal class DownloadsDirectoryPath : ValueOf<string, DownloadsDirectoryPath> { }

  internal class DatabaseFilePath : ValueOf<string, DatabaseFilePath> { }

}
  