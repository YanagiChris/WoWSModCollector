﻿namespace WoWSModCollector
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.ThumbnailPictureBox = new System.Windows.Forms.PictureBox();
            this.ImportBtn = new System.Windows.Forms.Button();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.ThumbnailBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.ClientPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FolderBtn = new System.Windows.Forms.Button();
            this.ModsListView = new System.Windows.Forms.ListView();
            this.HeaderModName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VersionLabel = new System.Windows.Forms.Label();
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbnailPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mods";
            // 
            // ThumbnailPictureBox
            // 
            this.ThumbnailPictureBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ThumbnailPictureBox.Location = new System.Drawing.Point(455, 54);
            this.ThumbnailPictureBox.Name = "ThumbnailPictureBox";
            this.ThumbnailPictureBox.Size = new System.Drawing.Size(498, 477);
            this.ThumbnailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ThumbnailPictureBox.TabIndex = 2;
            this.ThumbnailPictureBox.TabStop = false;
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(69, 54);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(80, 23);
            this.ImportBtn.TabIndex = 3;
            this.ImportBtn.Text = "Import";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Location = new System.Drawing.Point(155, 54);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(80, 23);
            this.RemoveBtn.TabIndex = 4;
            this.RemoveBtn.Text = "Remove";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // ThumbnailBtn
            // 
            this.ThumbnailBtn.Location = new System.Drawing.Point(355, 54);
            this.ThumbnailBtn.Name = "ThumbnailBtn";
            this.ThumbnailBtn.Size = new System.Drawing.Size(80, 23);
            this.ThumbnailBtn.TabIndex = 5;
            this.ThumbnailBtn.Text = "Thumbnail";
            this.ThumbnailBtn.UseVisualStyleBackColor = true;
            this.ThumbnailBtn.Click += new System.EventHandler(this.ThumbnailBtn_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Location = new System.Drawing.Point(873, 537);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(80, 23);
            this.ApplyBtn.TabIndex = 7;
            this.ApplyBtn.Text = "Apply";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // ClientPathTextBox
            // 
            this.ClientPathTextBox.Location = new System.Drawing.Point(14, 24);
            this.ClientPathTextBox.Name = "ClientPathTextBox";
            this.ClientPathTextBox.Size = new System.Drawing.Size(853, 19);
            this.ClientPathTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "WoWS client folder (C:\\Games\\World_of_Warships(_ASIA))";
            // 
            // FolderBtn
            // 
            this.FolderBtn.Location = new System.Drawing.Point(873, 22);
            this.FolderBtn.Name = "FolderBtn";
            this.FolderBtn.Size = new System.Drawing.Size(80, 23);
            this.FolderBtn.TabIndex = 2;
            this.FolderBtn.Text = "Folder";
            this.FolderBtn.UseVisualStyleBackColor = true;
            this.FolderBtn.Click += new System.EventHandler(this.FolderBtn_Click);
            // 
            // ModsListView
            // 
            this.ModsListView.BackColor = System.Drawing.SystemColors.Control;
            this.ModsListView.CheckBoxes = true;
            this.ModsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderModName});
            this.ModsListView.FullRowSelect = true;
            this.ModsListView.GridLines = true;
            this.ModsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ModsListView.HideSelection = false;
            this.ModsListView.Location = new System.Drawing.Point(12, 83);
            this.ModsListView.MultiSelect = false;
            this.ModsListView.Name = "ModsListView";
            this.ModsListView.Size = new System.Drawing.Size(435, 448);
            this.ModsListView.TabIndex = 6;
            this.ModsListView.UseCompatibleStateImageBehavior = false;
            this.ModsListView.View = System.Windows.Forms.View.Details;
            this.ModsListView.SelectedIndexChanged += new System.EventHandler(this.ModsListView_SelectedIndexChanged);
            this.ModsListView.Leave += new System.EventHandler(this.ModsListView_Leave);
            // 
            // HeaderModName
            // 
            this.HeaderModName.Text = "ModName";
            this.HeaderModName.Width = 430;
            // 
            // VersionLabel
            // 
            this.VersionLabel.Location = new System.Drawing.Point(12, 542);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(60, 12);
            this.VersionLabel.TabIndex = 8;
            // 
            // LinkLabel
            // 
            this.LinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LinkLabel.Location = new System.Drawing.Point(78, 539);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(683, 18);
            this.LinkLabel.TabIndex = 9;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "Download the latest version ";
            this.LinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(767, 537);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 569);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ModsListView);
            this.Controls.Add(this.FolderBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClientPathTextBox);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.ThumbnailBtn);
            this.Controls.Add(this.RemoveBtn);
            this.Controls.Add(this.ImportBtn);
            this.Controls.Add(this.ThumbnailPictureBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "WoWSModCollector";
            this.Load += new System.EventHandler(this.Application_ApplicationExit);
            ((System.ComponentModel.ISupportInitialize)(this.ThumbnailPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ThumbnailPictureBox;
        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.Button ThumbnailBtn;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.TextBox ClientPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FolderBtn;
        private System.Windows.Forms.ListView ModsListView;
        private System.Windows.Forms.ColumnHeader HeaderModName;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.LinkLabel LinkLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

