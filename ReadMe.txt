Summary:
Application which watches / finds duplicate videos based on either name or size.


Description:

Currently all single threaded.

Employs two file system watchers. 

One watches for downloads with a duplicate name to an existing video. 
This is to simplify the fact that the initial 'Created' size of a download, which triggers the watcher, may not be the actual final size of the completed download.

The other watches the library directory tree for videos of the same size.
This is to catch downloads which failed the name-match and are subsequently moved in to the library, or are moved into the library from elsewhere.
This assumes videos are not directly downloaded into the library.

Both watchers simply add items to a queue for processing on a timer. This is done because docs state that the watcher events should be as quick as possible so as
to not drop events.

Additionally the user can type a file name for which they wish to search for existence. This name should exclude the file extension.
The reason for this is two-fold. Firstly the file may have already been converted to another format and secondly because it helps with sites which show you the name
but not the extension of a file before downloading.

The timer poll internal will initially be long but will be shorter as long as there are still items in the queue.
In order to attempt not to interrupt the watchers only a single item will be process from the queue on each timer 'tick'.


Aside:

How-To: Make FlowLayoutPanel behave like a StackPanel
See: http://www.philosophicalgeek.com/2008/12/12/an-easy-stack-layout-panel-for-winforms/

Basically just set:

  AutoScroll = True
  FlowDirection = TopDown
  WrapContents = False

There appears to be issues when setting the Anchor for controls contained by a FlowLayoutPanel.
Here I simply set the child control widths when created and inside the FlowLayoutPanel.Resized event.