using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoWSModCollector.ModClass;

namespace WoWSModCollector
{
    public partial class Form1 : Form
    {
        readonly string settingFilePath = Directory.GetCurrentDirectory() + Common.settingFileName;
        readonly string myResModsPath = Directory.GetCurrentDirectory() + Common.resModsName;
        readonly string myThumbPath = Directory.GetCurrentDirectory() + Common.ThumbName;
        private readonly MyMods myMods;

        public Form1()
        {
            InitializeComponent();

            ThumbnailPictureBox.SizeMode = PictureBoxSizeMode.Zoom;


            if (Directory.Exists(myResModsPath) == false){
                Directory.CreateDirectory(myResModsPath);
            }

            XmlManager xmlManager = new XmlManager(settingFilePath);

            //設定ファイルの情報を呼び出す
            myMods = xmlManager.XmlReader();
            
            //設定の反映
            ClientPathTextBox.Text = myMods.ClientFolder;
            
            foreach(ModData modData in myMods.ModList)
            {
                ModsListBox.Items.Add(modData.Title);
            }

        }

        private void FolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = @"Select WoWS Client Folder. (C:\Games\World_of_Warships(_ASIA))",
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\Windows",
                ShowNewFolderButton = true
            };

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                string cFolder = fbd.SelectedPath;
                //選択されたフォルダを表示する
                ClientPathTextBox.Text = cFolder;

                //XMLファイルに保存
                myMods.ClientFolder = cFolder;
                XmlManager xmlManager = new XmlManager(settingFilePath);
                xmlManager.XmlWriter(myMods);

            }
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            string binFolder = ClientPathTextBox.Text + Common.binName;

            if (Directory.Exists(binFolder) == false)
            {
                MessageBox.Show("Can't read WoWS Client Folder.");
                return;
            }

            if (Directory.Exists(myResModsPath) == false)
            {
                MessageBox.Show("Can't read res_mods Folder in application folder.");
                return;
            }

            var folderList = Directory.GetDirectories(binFolder);

            if (folderList.Count() == 0)
            {
                MessageBox.Show("No folder in bin folder.");
                return;
            }

            //降順に並び替え
            Array.Sort(folderList);
            Array.Reverse(folderList);

            string targetFolder = folderList.First();
            targetFolder += Common.resModsName;

            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(myResModsPath, targetFolder, true);

            MessageBox.Show("Success.");
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = @"Select mod folder under 'res_mods'. (content/PnFMods/banks/...)",
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"C:\Windows",
                ShowNewFolderButton = true
            };

            //ダイアログを表示する
            if (fbd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            //選択されたフォルダ
            string modFolder = fbd.SelectedPath;

            string parentModFolder = Directory.GetParent(modFolder).Name;

            string title = Interaction.InputBox("Input Mod title", "Mod title", parentModFolder);

            if(title == "")
            {
                return;
            }

            string appModFolder = myResModsPath + "\\" + Path.GetFileName(modFolder);

            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(modFolder, appModFolder, true);

            List<string> files = Directory.GetFiles(modFolder, "*", System.IO.SearchOption.AllDirectories).ToList();

            List<string> replacedFiles = new List<string>();

            foreach (string file in files)
            {
                string replacedfile = file.Replace(modFolder, appModFolder);
                replacedFiles.Add(replacedfile);
            }

            replacedFiles.Sort();

            ModData modData = new ModData(title, replacedFiles);

            myMods.ModList.Add(modData);

            XmlManager xmlManager = new XmlManager(settingFilePath);
            xmlManager.XmlWriter(myMods);

            RefreshListBox();

            MessageBox.Show("Imported.");


        }

        private void RefreshListBox()
        {
            ModsListBox.Items.Clear();
            myMods.ModList.Sort();
            RemovePictureBox();

            foreach (ModData modData in myMods.ModList)
            {
                ModsListBox.Items.Add(modData.Title);
            }

        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            int removeIdx = ModsListBox.SelectedIndex;

            if(removeIdx == -1)
            {
                MessageBox.Show("Select target Mod.");
                return;
            }

            ModsListBox.Items.RemoveAt(removeIdx);

            List<string> removeFiles = myMods.ModList.ElementAt(removeIdx).fileList;

            RemoveAppMod(removeFiles);
            RemoveClientMod(removeFiles);

            string thumbName = myMods.ModList.ElementAt(removeIdx).ThumbFileName;

            if (File.Exists(thumbName))
            {
                File.Delete(myMods.ModList.ElementAt(removeIdx).ThumbFileName);
            }

            myMods.ModList.RemoveAt(removeIdx);

            XmlManager xmlManager = new XmlManager(settingFilePath);
            xmlManager.XmlWriter(myMods);

            RefreshListBox();

            MessageBox.Show("Removed.");

        }

        private void RemoveAppMod(List<string> appFiles)
        {
            foreach(string file in appFiles)
            {
                if (File.Exists(file)){
                    File.Delete(file);
                }
            }

        }

        private void RemoveClientMod(List<string> appFiles)
        {
            string modPath = GetWoWSClientModPath();

            if (modPath == "")
            {
                return;
            }

            foreach (string file in appFiles)
            {
                string replacedFile = file.Replace(myResModsPath, modPath);

                if (File.Exists(replacedFile)){
                    File.Delete(replacedFile);
                }
                
            }

        }

        private string GetWoWSClientModPath()
        {
            string binFolder = ClientPathTextBox.Text + "\\bin";
            
            if (Directory.Exists(binFolder) == false)
            {
                MessageBox.Show("Fail to read WoWS client folder");
                return "";
            }

            var folderList = Directory.GetDirectories(binFolder);

            if (folderList.Count() == 0)
            {
                MessageBox.Show("No folder in 'bin' folder");
                return "";
            }

            //降順に並び替え
            Array.Sort(folderList);
            Array.Reverse(folderList);

            string targetFolder = folderList.First();
            targetFolder += Common.resModsName;

            return targetFolder;

        }

        private void ModsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            RemovePictureBox();

            int selectedIdx = ModsListBox.SelectedIndex;

            if(selectedIdx == -1)
            {
                return;
            }

            //設定ファイルの情報を呼び出す
            XmlManager xmlManager = new XmlManager(settingFilePath);
            MyMods myMods = xmlManager.XmlReader();

            ModData modData = myMods.ModList.ElementAt(selectedIdx);

            string fileName = modData.ThumbFileName;

            if (fileName == "")
            {
                RemovePictureBox();
            }
            else
            {
                ThumbnailPictureBox.ImageLocation = modData.ThumbFileName;
            }

        }

        private void ThumbnailBtn_Click(object sender, EventArgs e)
        {

            int sIdx = ModsListBox.SelectedIndex;

            if (sIdx == -1)
            {
                MessageBox.Show("Select target Mod.");
                return;
            }

            OpenFileDialog fd = new OpenFileDialog() {
                InitialDirectory = myResModsPath,
                Title = "Select thumbnail",
                Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png"
            };

            //ダイアログを表示する
            if (fd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            //選択された画像
            string imgName = fd.FileName;
            string myThumbImg = myThumbPath + "\\" + sIdx + "_" + Path.GetFileName(imgName);

            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(imgName, myThumbImg, true);

            ThumbnailPictureBox.ImageLocation = myThumbImg;

            myMods.ModList.ElementAt(sIdx).ThumbFileName = myThumbImg;

            XmlManager xmlManager = new XmlManager(settingFilePath);
            xmlManager.XmlWriter(myMods);

        }

        private void RemovePictureBox()
        {
            if (ThumbnailPictureBox.Image != null)
            {
                ThumbnailPictureBox.Image.Dispose();
                ThumbnailPictureBox.Image = null;
            }
        }

        static void DeleteIfEmpty(String folder)
        {
                 foreach (var subdir in Directory.GetDirectories(folder))
                    DeleteIfEmpty(subdir);

                 if (IsDirectoryEmpty(folder))
                    Directory.Delete(folder);
        }

        private static bool IsDirectoryEmpty(string path)
        {
                 return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            DeleteIfEmpty(myResModsPath);
            Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
        }
    }
}
