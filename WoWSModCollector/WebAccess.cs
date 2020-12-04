//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

namespace WoWSModCollector
{
    class WebAccess
    {
        public static bool IsLatestVersion()
        {
            string url = "https://drive.google.com/uc?id=" + Common.FileID;

            System.Net.WebClient wc = new System.Net.WebClient();

            string result = wc.DownloadString(url);

            wc.Dispose();

            if (result != Common.Version)
            {
                return false;
            }

            return true;
        }
        

    }
}
