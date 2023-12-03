using DuplicateVidDetector.Models;
using PW.Events;
using PW.IO.FileSystemObjects;
using PW.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DuplicateVidDetector;

static class Program
{
  // HARDCODE: Directory path
  public static DirectoryPath PornLibraryDirectory { get; } = (DirectoryPath)@"P:\Porn\";

  // HARDCODE: Directory path
  public static DirectoryPath GameLibraryDirectory { get; } = (DirectoryPath)@"P:\From Games\";


  // HARDCODE: Store File path
  public static FilePath DbPath { get; } = (FilePath)@"P:\DB\DuplicateVidDetector.litedb";

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

    if (PornLibraryDirectory.Exists) Application.Run(container.Resolve<MainForm>()); // Required to set Init property
    else MsgBox.ShowError("Unable to continue. Library path does not exist: " + PornLibraryDirectory);
  }

  private static void PerformRegistrations(this IUnityContainer container)
  {
    container.RegisterType<MainForm>();
    container.RegisterInstance<IEvents>(new PW.Events.Events());
    container.RegisterInstance(new DataStore(DbPath, new DataMapper()));
  }

  public static IEnumerable<VideoFile> EnumerateAllVideos => PornLibraryDirectory.EnumerateVideoFiles()
          .Concat(GameLibraryDirectory.EnumerateVideoFiles());


}
