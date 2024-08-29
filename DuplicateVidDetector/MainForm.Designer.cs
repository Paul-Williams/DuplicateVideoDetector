
namespace DuplicateVidDetector
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      LibraryWatcher = new System.IO.FileSystemWatcher();
      CheckExistsTextBox = new PW.WinForms.Controls.CueBannerTextBox();
      FindVideoWithSameNameButton = new System.Windows.Forms.Button();
      SearchPanel = new System.Windows.Forms.Panel();
      RefreshButton = new System.Windows.Forms.Button();
      ScanningLabel = new PW.WinForms.Controls.TransparentLabel();
      GroupsPanel = new System.Windows.Forms.TableLayoutPanel();
      QueueProcessorTimer = new System.Windows.Forms.Timer(components);
      ((System.ComponentModel.ISupportInitialize)LibraryWatcher).BeginInit();
      SearchPanel.SuspendLayout();
      SuspendLayout();
      // 
      // LibraryWatcher
      // 
      LibraryWatcher.EnableRaisingEvents = true;
      LibraryWatcher.IncludeSubdirectories = true;
      LibraryWatcher.NotifyFilter = System.IO.NotifyFilters.FileName;
      LibraryWatcher.Path = "P:\\Porn";
      LibraryWatcher.SynchronizingObject = this;
      LibraryWatcher.Created += LibraryWatcher_Created;
      // 
      // CheckExistsTextBox
      // 
      CheckExistsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      CheckExistsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      CheckExistsTextBox.CueBannerText = "Type file name (without extension) to check if it already exists";
      CheckExistsTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      CheckExistsTextBox.Location = new System.Drawing.Point(3, 7);
      CheckExistsTextBox.Name = "CheckExistsTextBox";
      CheckExistsTextBox.Size = new System.Drawing.Size(621, 25);
      CheckExistsTextBox.TabIndex = 0;
      // 
      // FindVideoWithSameNameButton
      // 
      FindVideoWithSameNameButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      FindVideoWithSameNameButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      FindVideoWithSameNameButton.Location = new System.Drawing.Point(630, 6);
      FindVideoWithSameNameButton.Name = "FindVideoWithSameNameButton";
      FindVideoWithSameNameButton.Size = new System.Drawing.Size(43, 27);
      FindVideoWithSameNameButton.TabIndex = 1;
      FindVideoWithSameNameButton.Text = "Find";
      FindVideoWithSameNameButton.UseVisualStyleBackColor = true;
      FindVideoWithSameNameButton.Click += FindVideoWithSameNameButton_Click;
      // 
      // SearchPanel
      // 
      SearchPanel.BackColor = System.Drawing.SystemColors.Control;
      SearchPanel.Controls.Add(RefreshButton);
      SearchPanel.Controls.Add(CheckExistsTextBox);
      SearchPanel.Controls.Add(FindVideoWithSameNameButton);
      SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
      SearchPanel.Location = new System.Drawing.Point(0, 0);
      SearchPanel.Name = "SearchPanel";
      SearchPanel.Size = new System.Drawing.Size(720, 42);
      SearchPanel.TabIndex = 2;
      // 
      // RefreshButton
      // 
      RefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      RefreshButton.Font = new System.Drawing.Font("Wingdings 3", 12F);
      RefreshButton.Location = new System.Drawing.Point(680, 7);
      RefreshButton.Name = "RefreshButton";
      RefreshButton.Size = new System.Drawing.Size(28, 26);
      RefreshButton.TabIndex = 2;
      RefreshButton.Text = "P";
      RefreshButton.UseVisualStyleBackColor = true;
      RefreshButton.Click += RefreshButton_Click;
      // 
      // ScanningLabel
      // 
      ScanningLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      ScanningLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F);
      ScanningLabel.Location = new System.Drawing.Point(12, 147);
      ScanningLabel.Name = "ScanningLabel";
      ScanningLabel.Size = new System.Drawing.Size(696, 37);
      ScanningLabel.TabIndex = 4;
      ScanningLabel.TabStop = false;
      ScanningLabel.Text = "Enumerating Video Files...";
      ScanningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // GroupsPanel
      // 
      GroupsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      GroupsPanel.AutoScroll = true;
      GroupsPanel.AutoScrollMargin = new System.Drawing.Size(10, 0);
      GroupsPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
      GroupsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      GroupsPanel.Location = new System.Drawing.Point(3, 48);
      GroupsPanel.Name = "GroupsPanel";
      GroupsPanel.Size = new System.Drawing.Size(717, 300);
      GroupsPanel.TabIndex = 3;
      // 
      // QueueProcessorTimer
      // 
      QueueProcessorTimer.Interval = 5000;
      QueueProcessorTimer.Tick += QueueProcessorTimer_Tick;
      // 
      // MainForm
      // 
      AcceptButton = FindVideoWithSameNameButton;
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(720, 352);
      Controls.Add(ScanningLabel);
      Controls.Add(GroupsPanel);
      Controls.Add(SearchPanel);
      Font = new System.Drawing.Font("Segoe UI", 9.75F);
      KeyPreview = true;
      Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      Name = "MainForm";
      Text = "Duplicate Video Detector";
      Load += MainForm_Load;
      KeyDown += MainForm_KeyDown;
      ((System.ComponentModel.ISupportInitialize)LibraryWatcher).EndInit();
      SearchPanel.ResumeLayout(false);
      SearchPanel.PerformLayout();
      ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button FindVideoWithSameNameButton;
    private PW.WinForms.Controls.CueBannerTextBox CheckExistsTextBox;
    private System.Windows.Forms.Panel SearchPanel;
    private System.IO.FileSystemWatcher LibraryWatcher;
    private PW.WinForms.Controls.TransparentLabel ScanningLabel;
    private System.Windows.Forms.Button RefreshButton;
    private System.Windows.Forms.TableLayoutPanel GroupsPanel;
    private System.Windows.Forms.Timer QueueProcessorTimer;
  }
}

