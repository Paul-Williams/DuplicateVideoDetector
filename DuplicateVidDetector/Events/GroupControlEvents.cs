#nullable enable

using Prism.Events;
using PW.IO.FileSystemObjects;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DuplicateVidDetector.Events.GroupControlEvents
{
  internal class IgnoreFileGroup : PubSubEvent<IEnumerable<FilePath>> { }

  internal  class RemoveGroupEvent : PubSubEvent<GroupControl> { }

  internal class FileDeletedEvent : PubSubEvent<FilePath> { }

}
