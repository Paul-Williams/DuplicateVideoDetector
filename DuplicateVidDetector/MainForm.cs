﻿using DuplicateVidDetector.Events.GroupControlEvents;
using DuplicateVidDetector.Models;
using PW.Exceptions;
using PW.Extensions;
using PW.IO.FileSystemObjects;
using PW.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using static DuplicateVidDetector.FileExtensionExtensions;

namespace DuplicateVidDetector;

public partial class MainForm : Form
{
  #region Prism/Unity Support

  // This would be 'better' as a constructor: MainForm (IEventAggregator PrismEvents, DataStore StoreFile)
  [InjectionMethod]
  public void Init(IUnityContainer container)
  {
    Resolver = container;
    PrismEvents = container.Resolve<PW.Events.IEvents>();
    StoreFile = Resolver.Resolve<DataStore>();
  }

  private PW.Events.IEvents PrismEvents { get; set; }

  private IUnityContainer Resolver { get; set; }

  #endregion

  private VideoCollection Videos { get; } = [];

  private Queue<QueueItem> Queue { get; } = new Queue<QueueItem>();

  /// <summary>
  /// Used to store ignored duplicates.
  /// </summary>
  private DataStore StoreFile { get; set; }

  public MainForm()
  {
    InitializeComponent();
    PrismEvents = null!;
    Resolver = null!;
    StoreFile = null!;
    Icon = Properties.Resources.Duplicate1;
    LibraryWatcher.Path = Program.PornLibraryDirectory.ToString();
  }

  /// <summary>
  /// Handles forms' load event
  /// </summary>
  private async void MainForm_Load(object sender, EventArgs e)
  {
    try
    {
      // Ensure form is displayed and all? messages are processed, before awaiting task.
      Show();
      Application.DoEvents();
      Application.DoEvents();

      await RefreshListOfVideosAndDisplayDuplicates();
      SubscribeToEvents();
      QueueProcessorTimer.Enabled = true;
    }
    catch (Exception ex) { MsgBox.ShowError(ex); }
  }


  private async Task RefreshListOfVideosAndDisplayDuplicates()
  {
    ScanningLabel.Visible = true;
    Application.DoEvents();
    Application.DoEvents();

    // Dump any previous stuff.
    Videos.Clear();
    GroupsPanel.Controls.Clear();

    // Obtain a array of all videos from both 'library' directories and their sub-directories
    Videos.AddRange(await Task.Run(() => Program.EnumerateAllVideos).ConfigureAwait(true));

    GroupsPanel.Visible = false;
    GroupsPanel.SuspendLayout();
    CreateGroupControls(Videos.OfSameSize);
    //CreateGroupControls(Videos.WithSameName);
    GroupsPanel.ResumeLayout();
    GroupsPanel.Visible = true;
    ScanningLabel.Visible = false;
  }

  /// <summary>
  /// Subscribes to PubSub events to be handled
  /// </summary>
  private void SubscribeToEvents()
  {
    PrismEvents.GetEvent<IgnoreFileGroup>().Subscribe(files => StoreFile.Upsert(new IgnoredGroup(files)));

    PrismEvents.GetEvent<RemoveGroupEvent>().Subscribe(group => GroupsPanel.Controls.Remove(group));

    PrismEvents.GetEvent<FileDeletedEvent>().Subscribe(file => Videos.RemoveAll(x => x.FilePath == file));
  }

  /// <summary>
  /// Creates a <see cref="GroupControl"/> for each group in the enumeration.
  /// </summary>
  private void CreateGroupControls(IEnumerable<IGrouping<long, VideoFile>> videoGroups)
  {
    static string CreateTitle(int count, long fileSize) =>
      $"File Count: {count} - File Size: {fileSize.ToStringByteSize()} ({fileSize} Bytes)";

    foreach (var group in videoGroups.OrderByDescending(x => x.Key))
    {
      var filePaths = group.Select(videoFile => videoFile.FilePath).ToArray();
      if (!StoreFile.All<IgnoredGroup>().Any(ignoredGroup => ignoredGroup.Matches(filePaths)))
      {
        CreateGroupControl(CreateTitle(filePaths.Length, group.Key), filePaths);
      }
    }
  }

  /// <summary>
  /// Creates a <see cref="GroupControl"/> for each group in the enumeration.
  /// </summary>
  private void CreateGroupControls(IEnumerable<IGrouping<FileNameWithoutExtension, VideoFile>> videoGroups)
  {
    static string CreateTitle(int count, FileNameWithoutExtension file) =>
      $"File Count: {count} - File name: {file.Value}";

    foreach (var group in videoGroups.OrderByDescending(x => x.Key))
    {
      var filePaths = group.Select(videoFile => videoFile.FilePath).ToArray();
      if (!StoreFile.All<IgnoredGroup>().Any(ignoredGroup => ignoredGroup.Matches(filePaths)))
      {
        CreateGroupControl(CreateTitle(filePaths.Length, group.Key), filePaths);
      }
    }
  }

  /// <summary>
  /// Creates a single <see cref="GroupControl"/>.
  /// </summary>
  private void CreateGroupControl(string title, IEnumerable<FilePath> videos, bool ignoreButtonVisible = true)
  {
    var ctl = Resolver.Resolve<GroupControl>(); // Required to inject event aggregator.

    ctl.Title = title;
    ctl.BackColor = SystemColors.Control;
    ctl.IgnoreButtonVisible = ignoreButtonVisible;
    ctl.AddRange(videos);
    GroupsPanel.Controls.Add(ctl);
    ctl.Dock = DockStyle.Fill;
  }

  /// <summary>
  /// Handles events for new files being created. Checks for duplicates BASED ON NAME.
  /// </summary>
  private void DownloadWatcher_Created(object sender, FileSystemEventArgs e)
  {
    try
    {
      if (IsVideo(Path.GetExtension(e.FullPath)))
      {
        // Matching by name, here, seems like a hack-workaround for the fact that the file 
        // probably hasn't finished downloading, so preventing us matching on size.
        Queue.Enqueue(new QueueItem(new VideoFile((FilePath)e.FullPath), MatchByOption.Name));
        QueueProcessorTimer.Enabled = true;
      }
    }
    catch (Exception ex)
    {
      MsgBox.ShowError(ex);
    }
  }

  private void FindVideoWithSameNameButton_Click(object sender, EventArgs e)
  {
    try
    {
      var query = CheckExistsTextBox.Text;
      if (query.Length == 0) return;
      var matches = query.Contains('*')
        ? Videos.Find(query)
        : Videos.Find((FileNameWithoutExtension)query);

      if (matches.Count != 0)
      {
        GroupsPanel.Controls.Clear();
        CreateGroupControl(query, matches.Select(x => x.FilePath), false);
      }

    }
    catch (InvalidFileNameException ex) { MsgBox.ShowError(ex, "Invalid file name"); }
    catch (Exception ex) { MsgBox.ShowError(ex); }
  }


  private async void RefreshButton_Click(object sender, EventArgs e)
  {
    try { await RefreshListOfVideosAndDisplayDuplicates(); }
    catch (Exception ex) { MsgBox.ShowError(ex); }
  }

  /// <summary>
  /// Process queued items.
  /// </summary>
  private void QueueProcessorTimer_Tick(object sender, EventArgs e)
  {
    // This logic appears bugged.
    // * Only processes single item each run-through, rather than looping to process all.
    // * Timer disabled to prevent re-entry, however is also re-enabled in LibraryWatcher_Created

    if (QueueProcessorTimer.Enabled == false) return;   // Prevent re-entry
    QueueProcessorTimer.Enabled = false;                // Prevent re-entry
    if (Queue.Count == 0) return;                       // Sanity check

    try
    {
      var item = Queue.Dequeue();

      // If 'item' was transitory and no longer exists, then ignore it.
      if (!item.Video.FilePath.Exists) return;

      var matches = item.MatchBy == MatchByOption.Name ? Videos.Find(item.Video.FilePath.Name) : Videos.FindBySize(item.Video.Size);

      if (matches.Count != 0)
      {
        var title = "Duplicate: " + (item.MatchBy == MatchByOption.Name ? "Name" : "Size");
        var files = matches.Select(x => x.FilePath).Prepend(item.Video.FilePath);
        CreateGroupControl(title, files);
        this.Flash();
      }
    }
    catch (Exception ex) { MsgBox.ShowError("Timer error: " + ex.Message); }
    finally { QueueProcessorTimer.Enabled = Queue.Count != 0; } // Only re-enable timer if there are more items.

  }

  private void LibraryWatcher_Created(object sender, FileSystemEventArgs e)
  {
    if (IsVideo(Path.GetExtension(e.FullPath)))
    {
      Queue.Enqueue(new QueueItem(new VideoFile((FilePath)e.FullPath), MatchByOption.Size));
      QueueProcessorTimer.Enabled = true;
    }

  }

  private void MainForm_KeyDown(object sender, KeyEventArgs e)
  {
    try
    {
      // [Ctrl + Alt + K] clears ignore list, if user agrees. 
      if (e.KeyCode == Keys.X && e.Control && e.Alt && MsgBox.ShowQuestion("Clear ignore list?") == MsgBox.QuestionResult.Yes)
        StoreFile.Drop<IgnoredGroup>();
    }
    catch (Exception ex)
    {
      MsgBox.ShowError(ex);
    }


  }

}