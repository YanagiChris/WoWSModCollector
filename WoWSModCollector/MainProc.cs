//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWSModCollector
{
    class MainProc
    {
        public readonly string settingFilePath = Directory.GetCurrentDirectory() + Common.SettingFileName;
        public readonly string myModsPath = Directory.GetCurrentDirectory() + Common.MyModsName;
        public readonly string myResModsPath = Directory.GetCurrentDirectory() + Common.ResModsName;
        public readonly string myThumbPath = Directory.GetCurrentDirectory() + Common.ThumbName;
        public ModClass.MyMods myMods;
        private XmlManager xmlManager;
        public Form1 form;

        public MainProc(Form1 form1)
        {
            form = form1;
            xmlManager = new XmlManager(settingFilePath);

            if (Directory.Exists(myModsPath) == false)
            {
                Directory.CreateDirectory(myModsPath);
            }

        }

        public string GetClientPath()
        {

            //設定ファイルの情報を呼び出す
            myMods = xmlManager.XmlReader();
            
            return myMods.ClientFolder;
        }

        public string[] GetAllMods()
        {
            int count = 0;
            string[] modNames = new string[myMods.ModList.Count];

            foreach (ModData modData in myMods.ModList)
            {
                modNames[count] = modData.Title;
            }

            return modNames;
        }

        public void ModApply(string wowsClientPath, string[] modNames)
        {

            string binFolder = wowsClientPath + Common.BinName;

            if (Directory.Exists(binFolder) == false)
            {
                MessageBox.Show("Can't read WoWS Client Folder.");
                return;
            }

            if (Directory.Exists(myModsPath) == false)
            {
                MessageBox.Show("Can't read MyMods Folder in application folder.");
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
            targetFolder += Common.ResModsName;

            if (CopyApplyMods(modNames, targetFolder, form))
            {
                MessageBox.Show("Success.");
            }

            return;
        }
        private bool CopyApplyMods(string[] modNames, string targetFolder, Form1 form)
        {

            form.SetProgressStatus(modNames.Length, true);

            int i = 0;

            foreach (string modName in modNames)
            {

                form.ChangeProgressStatus(i);
                i++;

                string copyModFolder = myModsPath + "\\" + modName;

                string[] modFiles = Directory.GetFiles(copyModFolder);

                try
                {

                    if(Common.CopyFolder(copyModFolder, targetFolder) == false)
                    {
                        form.DisableProgressBar();
                        return false;
                    }

                    foreach (string modFile in modFiles)
                    {
                        if (Common.CopyFile(modFile, targetFolder + Path.GetFileName(modFile)) == false)
                        {
                            form.DisableProgressBar();
                            return false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("File copy error.");
                    form.DisableProgressBar();
                    return false;
                }
            }

            form.DisableProgressBar();
            return true;

        }

        public void WriteClientPathXml(string clientPath)
        {

            if (xmlManager.CheckSettingFile() == false)
            {
                return;
            }

            //XMLファイルに保存
            myMods.ClientFolder = clientPath;
            xmlManager.XmlWriter(myMods);
        }

        private bool CheckModTitle(string titleName)
        {
            if(titleName == "")
            {
                return false;
            }

            string[] modTitles = new string[myMods.ModList.Count];
            int count = 0;

            foreach(ModData modData in myMods.ModList)
            {
                modTitles[count] = modData.Title;
                count++;
            }

            if (modTitles.Contains(titleName))
            {
                MessageBox.Show("Exists the same mod name.");
                return false;
            }

            //ファイル名に使用できない文字を取得
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();

            if (titleName.IndexOfAny(invalidChars) >= 0)
            {
                MessageBox.Show("Invalid letters. (" + invalidChars + ")");
                return false;
            }

                return true;

        }

        public bool ImportMod(string modFolder)
        {
            if (xmlManager.CheckSettingFile() == false)
            {
                return false;
            }

            if (modFolder == "")
            {
                return false;
            }

            string parentModFolder = Directory.GetParent(modFolder).Name;

            string title = Interaction.InputBox("Input Mod title", "Mod title", parentModFolder);

            if (CheckModTitle(title) == false)
            {
                return false;
            }

            string appModFolder = myModsPath + "\\" + title + "\\" + Path.GetFileName(modFolder);

            if(Common.CopyFolder(modFolder, appModFolder) == false)
            {
                return false;
            }

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

            xmlManager.XmlWriter(myMods);

            return true;
        }

        public bool RemoveMod(int removeIdx)
        {
            if (xmlManager.CheckSettingFile() == false)
            {
                return false;
            }

            string[] removeFiles = myMods.ModList.ElementAt(removeIdx).fileList.ToArray();
            string clientPath = GetWoWSClientModPath(myMods.ClientFolder);

            if (clientPath == ""){
                return false;
            }

            if (Common.DeleteFiles(removeFiles) == false)
            {
                return false;
            }

            if (RemoveClientMod(removeIdx) == false) {
                return false;
            }

            string thumbName = myMods.ModList.ElementAt(removeIdx).ThumbFileName;

            if (Common.DeleteFile(thumbName) == false)
            {
                return false;
            }

            myMods.ModList.RemoveAt(removeIdx);

            xmlManager.XmlWriter(myMods);

            return true;
        }

        public bool RemoveClientMod(int modIdx)
        {
            string resmodsPath = GetWoWSClientModPath(myMods.ClientFolder);

            if (Directory.Exists(resmodsPath) == false)
            {
                MessageBox.Show("Fail to read a res_mod folder");
                return false;
            }

            string[] appFiles = myMods.ModList.ElementAt(modIdx).fileList.ToArray();

            string modName = myMods.ModList.ElementAt(modIdx).Title;

            foreach (string file in appFiles)
            {
                string replacedFile = file.Replace(Path.Combine(myModsPath, modName), resmodsPath);

                    if (Common.DeleteFile(replacedFile) == false)
                    {
                        return false;
                    }
            }

            return true;

        }

        public string GetThumbnailImgName(int idx)
        {
            if (xmlManager.CheckSettingFile() == false)
            {
                return "";
            }

            //前の画像の削除
            string beforeImg = myMods.ModList.ElementAt(idx).ThumbFileName;

            if (Common.DeleteFile(beforeImg) == false)
            {
                MessageBox.Show("Fail to delete a thumbnail file.");
                return "";
            }

            OpenFileDialog fd = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Title = "Select thumbnail",
                Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png"
            };

            //ダイアログを表示する
            if (fd.ShowDialog() != DialogResult.OK)
            {
                return "";
            }

            //選択された画像
            string imgName = fd.FileName;
            string myThumbImg = myThumbPath + "\\" + idx + "_" + Path.GetFileName(imgName);

            if (Common.CopyFile(imgName, myThumbImg) == false)
            {
                MessageBox.Show("Fail to copy a thumbnail file.");
                return "";
            }

            myMods.ModList.ElementAt(idx).ThumbFileName = myThumbImg;

            xmlManager.XmlWriter(myMods);

            return myThumbImg;
        }

        private string GetWoWSClientModPath(string clientPath)
        {
            string binFolder = clientPath + "\\bin";

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
            targetFolder += Common.ResModsName;

            return targetFolder;

        }

        public void SaveCheckedMods(ListView.CheckedIndexCollection idxs)
        {
            for(int i = 0; i < myMods.ModList.Count; i++)
            {
                if (idxs.Contains(i))
                {
                    myMods.ModList.ElementAt(i).ApplyChecked = true;
                }
                else
                {
                    myMods.ModList.ElementAt(i).ApplyChecked = false;
                }
            }

            xmlManager.XmlWriter(myMods);

        }
    }


}
