#nullable enable

using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;

namespace DuplicateVidDetector
{

  /// <summary>
  /// Extension methods for the <see cref="FileExtension"/> type.
  /// </summary>
  internal static class FileExtensionExtensions
  {

    /// <summary>
    /// Common video file extensions.
    /// </summary>
    public static HashSet<string> VideoFileExtensions { get; } =
      new HashSet<string>(
        new string[] { ".3g2", ".3gp", ".avi", ".divx", ".flv", ".h264", ".h265", ".m2v", ".m4p", ".m4v", ".mk3d", ".mkv", ".mov", ".mp2", ".mp4", ".mpeg", ".mpg", ".ogm", ".ogv", ".rm", ".rmvb", ".ts", ".vob", ".webm", ".wmv", ".xvid" },
        StringComparer.OrdinalIgnoreCase);


    // Previous:
    // ".mpg", ".mpeg", ".avi", ".mp4", ".mov", ".m4v", ".webm", ".mkv", ".wmv", ".m4p", ".m2v", ".mp2", ".3gp"

    // New: 
    //".3g2",  ".3gp",  ".avi", ".divx", ".flv", ".h264", ".h265", ".m2v",  ".m4p",  ".m4v", ".mk3d",  ".mkv",  ".mov",  ".mp2",  ".mp4",  ".mpeg", ".mpg", ".ogm", ".ogv", ".rm", ".rmvb", ".ts", ".vob",  ".webm",  ".wmv", ".xvid"



    ///// <summary>
    ///// Add a known video extension to the list. Returns true if added or false if already exists.
    ///// </summary>
    //public static bool AddVideoExtension(FileExtension ext) => VideoFileExtensions.Add(ext.Value);

    /// <summary>
    /// Determines whether the file extension is that of a video file.
    /// </summary>
    public static bool IsVideo(this FileExtension fileExtension) => VideoFileExtensions.Contains(fileExtension.Value);

    /// <summary>
    /// Determines whether the file extension is that of a video file.
    /// </summary>
    public static bool IsVideo(string fileExtension) => VideoFileExtensions.Contains(fileExtension);


  }

}
