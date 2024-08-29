namespace DuplicateVidDetector
{
  partial class GroupControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      Panel = new System.Windows.Forms.TableLayoutPanel();
      TitleLabel = new System.Windows.Forms.Label();
      IgnoreButton = new System.Windows.Forms.Button();
      SuspendLayout();
      // 
      // Panel
      // 
      Panel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      Panel.AutoSize = true;
      Panel.ColumnCount = 1;
      Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      Panel.Location = new System.Drawing.Point(0, 38);
      Panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      Panel.Name = "Panel";
      Panel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
      Panel.RowCount = 2;
      Panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      Panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      Panel.Size = new System.Drawing.Size(460, 46);
      Panel.TabIndex = 0;
      // 
      // TitleLabel
      // 
      TitleLabel.AutoSize = true;
      TitleLabel.Location = new System.Drawing.Point(5, 8);
      TitleLabel.Name = "TitleLabel";
      TitleLabel.Size = new System.Drawing.Size(32, 17);
      TitleLabel.TabIndex = 1;
      TitleLabel.Text = "Title";
      // 
      // IgnoreButton
      // 
      IgnoreButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      IgnoreButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      IgnoreButton.Location = new System.Drawing.Point(371, 4);
      IgnoreButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      IgnoreButton.Name = "IgnoreButton";
      IgnoreButton.Size = new System.Drawing.Size(87, 30);
      IgnoreButton.TabIndex = 2;
      IgnoreButton.Text = "Ignore";
      IgnoreButton.UseVisualStyleBackColor = true;
      IgnoreButton.Click += IgnoreButton_Click;
      // 
      // GroupControl
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(IgnoreButton);
      Controls.Add(TitleLabel);
      Controls.Add(Panel);
      Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
      Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
      Name = "GroupControl";
      Size = new System.Drawing.Size(462, 97);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel Panel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button IgnoreButton;
    }
}
