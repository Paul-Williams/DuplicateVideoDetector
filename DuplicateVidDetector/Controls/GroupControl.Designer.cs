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
      this.Panel = new System.Windows.Forms.TableLayoutPanel();
      this.TitleLabel = new System.Windows.Forms.Label();
      this.IgnoreButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // Panel
      // 
      this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.Panel.AutoSize = true;
      this.Panel.ColumnCount = 1;
      this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.Panel.Location = new System.Drawing.Point(0, 38);
      this.Panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Panel.Name = "Panel";
      this.Panel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
      this.Panel.RowCount = 2;
      this.Panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.Panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.Panel.Size = new System.Drawing.Size(460, 46);
      this.Panel.TabIndex = 0;
      // 
      // TitleLabel
      // 
      this.TitleLabel.AutoSize = true;
      this.TitleLabel.Location = new System.Drawing.Point(5, 8);
      this.TitleLabel.Name = "TitleLabel";
      this.TitleLabel.Size = new System.Drawing.Size(32, 17);
      this.TitleLabel.TabIndex = 1;
      this.TitleLabel.Text = "Title";
      // 
      // IgnoreButton
      // 
      this.IgnoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.IgnoreButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.IgnoreButton.Location = new System.Drawing.Point(371, 4);
      this.IgnoreButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.IgnoreButton.Name = "IgnoreButton";
      this.IgnoreButton.Size = new System.Drawing.Size(87, 30);
      this.IgnoreButton.TabIndex = 2;
      this.IgnoreButton.Text = "Ignore";
      this.IgnoreButton.UseVisualStyleBackColor = true;
      this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
      // 
      // GroupControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.IgnoreButton);
      this.Controls.Add(this.TitleLabel);
      this.Controls.Add(this.Panel);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "GroupControl";
      this.Size = new System.Drawing.Size(462, 97);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Panel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button IgnoreButton;
    }
}
