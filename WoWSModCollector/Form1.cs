//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WoWSModCollector
{
    public partial class Form1 : Form
    {

        private readonly MainProc mainProc;

        public Form1()
        {
            InitializeComponent();

            mainProc = new MainProc();

            FormInit();

            this.FormClosing += Form1_FormClosing;

        }

        private void FormInit()
        {
            VersionLabel.Text = Common.Version;

            //設定の反映
            ClientPathTextBox.Text = mainProc.GetClientPath();

            string[] modNames = mainProc.GetAllMods();

            if (WebAccess.IsLatestVersion())
            {
                LinkLabel.Visible = false;
            }
            else
            {
                LinkLabel.Text += Common.Version;
            }

            RefreshListBox();

            
        } 

        private void FolderBtn_Click(object sender, EventArgs e)
        {
            string description = @"Select WoWS Client Folder. (C:\Games\World_of_Warships(_ASIA))";

            string clientFolder = GetFolder(description);

            if (clientFolder == "")
            {
                return;
            }

            ClientPathTextBox.Text = clientFolder;

            //XMLファイルに保存
            mainProc.WriteClientPathXml(clientFolder);
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {

            ListView.CheckedIndexCollection checkedIdx = ModsListView.CheckedIndices;

            mainProc.SaveCheckedMods(checkedIdx);

            int[] deleteModIdxs = GetDeleteModIdxs();

            foreach(int idx in deleteModIdxs)
            {
                mainProc.RemoveClientMod(idx);
            }

            string[] applyModNames = GetApplyModNames();

            mainProc.ModApply(ClientPathTextBox.Text, applyModNames);
            
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {

            string description = @"Select a import mod folder. (content/gameplay/gui/PnFMods/banks/...)";

            string importFolder = GetFolder(description);

            if (mainProc.ImportMod(importFolder))
            {
                RefreshListBox();

            }

        }

        private void RefreshListBox()
        {
            ModsListView.Items.Clear();
            mainProc.myMods.ModList.Sort();
            RemovePictureBox();

            foreach (ModData modData in mainProc.myMods.ModList)
            {
                ModsListView.Items.Add(modData.Title);
                ModsListView.Items[ModsListView.Items.Count - 1].Checked = modData.ApplyChecked;
                ;

            }

        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (ModsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select target Mod.");
                return;
            }

            int removeIdx = ModsListView.SelectedItems[0].Index;

            bool result = mainProc.RemoveMod(removeIdx);

            if (result == false)
            {
                return;
            }

            ModsListView.Items.RemoveAt(removeIdx);

            RefreshListBox();

        }

        private void ModsListView_Leave(object sender, System.EventArgs e)
        {
          
            ListView.CheckedIndexCollection checkedIdx = ModsListView.CheckedIndices;

            mainProc.SaveCheckedMods(checkedIdx);

        }

        private void ModsListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            RemovePictureBox();

            if(ModsListView.SelectedItems.Count < 1)
            {
                return;
            }

            int sIdx = ModsListView.SelectedItems[0].Index;

            //設定ファイルの情報を呼び出す
            ModData modData = mainProc.myMods.ModList.ElementAt(sIdx);
            string fileName = modData.ThumbFileName;

            if (fileName == "")
            {
                RemovePictureBox();
            }
            else
            {
                ThumbnailPictureBox.ImageLocation = fileName;
            }

        }

        private void ThumbnailBtn_Click(object sender, EventArgs e)
        {

            if(ModsListView.Items.Count < 1)
            {
                MessageBox.Show("No Mod.");
                return;

            }

            if (ModsListView.SelectedItems.Count < 1)
            {
                MessageBox.Show("Select target Mod.");
                return;
            }

            int sIdx = ModsListView.SelectedItems[0].Index;


            string myThumbImg = mainProc.GetThumbnailImgName(sIdx);

            if(myThumbImg == "")
            {
                return;
            }

            ThumbnailPictureBox.ImageLocation = myThumbImg;

        }

        private void RemovePictureBox()
        {
            if (ThumbnailPictureBox.Image != null)
            {
                ThumbnailPictureBox.Image.Dispose();
                ThumbnailPictureBox.Image = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ListView.CheckedIndexCollection checkedIdx = ModsListView.CheckedIndices;

            mainProc.SaveCheckedMods(checkedIdx);

            Common.DeleteIfEmpty(mainProc.myModsPath);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {

            Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
        }

        private int[] GetDeleteModIdxs()
        {

            List<int> tgtIdxs = new List<int>();

            for (int i = 0; i < ModsListView.Items.Count; i++)
            {
                tgtIdxs.Add(i);
            }

            foreach (int checkedIdx in ModsListView.CheckedIndices)
            {
                if (tgtIdxs.Contains(checkedIdx))
                {
                    tgtIdxs.Remove(checkedIdx);
                }
            }

            return tgtIdxs.ToArray();

        }

        private string[] GetApplyModNames()
        {

            int modCont = ModsListView.CheckedItems.Count;

            if (modCont == 0)
            {
                return null;
            }

            string[] modsName = new string[modCont];
            int cnt = 0;

            foreach (ListViewItem itm in ModsListView.CheckedItems)
            {
                modsName[cnt] = itm.Text;
                cnt++;

            }

            return modsName;

        }

        public string GetFolder(string description)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\Windows",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }

            return "";
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(Common.DLUrl);
        }
    }
}
