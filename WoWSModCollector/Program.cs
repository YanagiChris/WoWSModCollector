//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using System;
using System.Windows.Forms;

namespace WoWSModCollector
{
    static class Program
    {
        
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
