using DuplicateVidDetector.Events.GroupControlEvents;
using PW.Extensions;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DuplicateVidDetector;


public partial class GroupControl : UserControl
{
  #region Prism/Unity Support

  [Unity.InjectionMethod]
  public void Init(IUnityContainer container) => IEvents = container.Resolve<PW.Events.IEvents>();

  private PW.Events.IEvents IEvents { get; set; }

  #endregion

  public GroupControl() {
    InitializeComponent();
    IEvents = null!;
  }

  private List<FilePath> ItemList { get; } = [];

  public string Title { get => TitleLabel.Text; set => TitleLabel.Text = value; }

  public bool IgnoreButtonVisible { get => IgnoreButton.Visible; set => IgnoreButton.Visible = value; } 


  public void AddRange(IEnumerable<FilePath> items) => items.ForEach(Add);

  public void Add(FilePath item) {
    ArgumentNullException.ThrowIfNull(item);

    var label = new LinkLabel {
      Text = item,
      Left = 0,
      Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
      AutoSize = true,
      LinkBehavior = LinkBehavior.HoverUnderline,
      UseMnemonic = false
    };

    label.LinkClicked += LinkLabel_LinkClicked;
    Panel.Controls.Add(label, 0, Panel.RowCount);
    Height = Panel.Height + Panel.Top;
    ItemList.Add(item);
  }

  private void LinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) {
    ArgumentNullException.ThrowIfNull(sender);

    var file = (FilePath)((LinkLabel)sender).Text;


    // Left button opens link
    if (e.Button == MouseButtons.Left) {
      try {
        Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
      }
      catch (Exception) { } // TODO: Should do something with this.
    }

    // Middle button find link using Windows Explorer
    else if (e.Button == MouseButtons.Middle) {
      if (file.Exists) file.SelectInExplorer();
    }


    // Right button sends link target to recycle bin
    else if (e.Button == MouseButtons.Right) {
      if (file.Exists) {
        file.Recycle();
        IEvents.GetEvent<FileDeletedEvent>().Publish(file);
      }

      // If that was the last duplicate, then request to remove the group.
      if (Panel.Controls.OfType<LinkLabel>().Where(x => x.Enabled).Count() == 2) IEvents.GetEvent<RemoveGroupEvent>().Publish(this);

      // Disable link once file is deleted.
      else {
        ((LinkLabel)sender).Enabled = false;
        ItemList.Remove(file);
      }
    }
  }

  // Prevent reporting these files as being duplicates.
  private void IgnoreButton_Click(object sender, EventArgs e) {
    IEvents.GetEvent<IgnoreFileGroup>().Publish(ItemList);
    IEvents.GetEvent<RemoveGroupEvent>().Publish(this);
  }
}
