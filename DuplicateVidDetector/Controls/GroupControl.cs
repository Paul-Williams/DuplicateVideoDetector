#nullable enable

using DuplicateVidDetector.Events.GroupControlEvents;
using PW.Extensions;
using PW.IO.FileSystemObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DuplicateVidDetector
{

  public partial class GroupControl : UserControl
  {
    #region Prism/Unity Support

    [Unity.InjectionMethod]
    public void Init(IUnityContainer container)
    {
      EA = container.Resolve<Prism.Events.IEventAggregator>();
      //UC = container;
    }

    private Prism.Events.IEventAggregator EA { get; set; }
    //private Unity.IUnityContainer UC { get; set; }

    #endregion

    public GroupControl()
    {
      InitializeComponent();
      EA = null!;
      //UC = null!;
    }

    private List<FilePath> ItemList { get; } = new List<FilePath>();

    public string Title { get => TitleLabel.Text; set => TitleLabel.Text = value; }

    public void AddRange(IEnumerable<FilePath> items) => items.ForEach(Add);

    public void Add(FilePath item)
    {
      if (item is null) throw new ArgumentNullException(nameof(item));

      var label = new LinkLabel
      {
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

    private void LinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
      if (sender is null) throw new ArgumentNullException(nameof(sender));  

      var file = (FilePath)((LinkLabel)sender).Text;


      // Left button opens link
      if (e.Button == MouseButtons.Left)
      {
        try
        {
          // BUGFIX - Now requires ProcessStartInfo
          var psi = new ProcessStartInfo(file) { UseShellExecute = true };
          Process.Start(psi);
        }
        catch (Exception) { }
      }

      // Middle button find link using WIndows Explorer
      else if (e.Button == MouseButtons.Middle)
      {
        if (file.Exists) file.SelectInExplorer();
      }

      // Right button sends link target to recycle bin
      else if (e.Button == MouseButtons.Right)
      {
        if (file.Exists)
        {
          file.Recycle();
          EA.GetEvent<FileDeletedEvent>().Publish(file);
        }

        // If that was the last duplicate, then request to remove the group.
        if (Panel.Controls.OfType<LinkLabel>().Where(x => x.Enabled).Count() == 2)
          EA.GetEvent<RemoveGroupEvent>().Publish(this);

        // Disable link once file is deleted.
        else
        {
          ((LinkLabel)sender).Enabled = false;
          ItemList.Remove(file);

        }
      }
    }


    private void IgnoreButton_Click(object sender, EventArgs e)
    {
      EA.GetEvent<IgnoreFileGroup>().Publish(ItemList);
      EA.GetEvent<RemoveGroupEvent>().Publish(this);
    }
  }
}
