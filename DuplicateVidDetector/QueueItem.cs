#nullable enable

using DuplicateVidDetector.Models;
using PW.IO.FileSystemObjects;

namespace DuplicateVidDetector
{
  internal class QueueItem
  {
    public QueueItem(VideoFile video, MatchBy matchBy)
    {
      Video = video;
      MatchBy = matchBy;
    }

    public VideoFile Video { get; }
    public MatchBy MatchBy { get; }
  }
}
