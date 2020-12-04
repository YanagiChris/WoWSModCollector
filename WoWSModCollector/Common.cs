//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//Modification of this source code is prohibited.

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWSModCollector
{
    static class Common
    {
        public static readonly string SettingFileName = "\\settings.config";
        public static readonly string ResModsName = "\\res_mods";
        public static readonly string ContentName = "\\content";
        public static readonly string PnfModsName = "\\PnFMods";
        public static readonly string BinName = "\\bin";
        public static readonly string ThumbName = "\\Thumb";
        public static readonly string MyModsName = "\\MyMods";
        public static readonly string Version = "v2.0.1";
        public static readonly string FileID = "1YQNZZKM84ljQlwRujnQduqfJDqxbDZXo";
        public static readonly string DLUrl = "https://yanagi-chris.booth.pm/items/2576787";


        public static bool DeleteFile(string fileName)
        {
            if(File.Exists(fileName) == false)
            {
                return true;
            }

            try
            {
                File.Delete(fileName);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void DeleteIfEmpty(String folder)
        {
            foreach (var subdir in Directory.GetDirectories(folder))
                DeleteIfEmpty(subdir);

            if (IsDirectoryEmpty(folder))
                Directory.Delete(folder);
        }

        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static bool DeleteFiles(string[] appFiles)
        {
            foreach (string file in appFiles)
            {
                if (Common.DeleteFile(file) == false)
                {
                    return false;
                }
            }

            return true;

        }

        public static bool CopyFolder(string copyFolder, string targetFolder)
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(copyFolder, targetFolder, true);
            }
            catch
            {
                MessageBox.Show("Failed folder copy. " + copyFolder + " to " + targetFolder);
                return false;
            }

            return true;
        }

        public static bool CopyFile(string copyFile, string targetFolder)
        {
            try
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(copyFile, targetFolder, true);
            }
            catch
            {
                MessageBox.Show("Failed file copy. " + copyFile + " to " + targetFolder);
                return false;
            }

            return true;

        }

    }


}
