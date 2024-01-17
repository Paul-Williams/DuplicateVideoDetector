using DuplicateVidDetector.Models;

namespace DuplicateVidDetector;

internal class QueueItem(VideoFile video, MatchByOption matchBy)
{
  public VideoFile Video { get; } = video;
  public MatchByOption MatchBy { get; } = matchBy;
}
