//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;

namespace WoWSModCollector
{
    public class ModData : System.IComparable
    {
        public string Title;
        public string ThumbFileName;
        public bool ApplyChecked;
        public List<string> fileList;

        public ModData()
        {
            Title = "";
            ThumbFileName = "";
            ApplyChecked = false;
            fileList = new List<string>();
        }

        public ModData(string title, List<string> files)
        {
            Title = title;
            fileList = files;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (this.GetType() != obj.GetType())
            {
                throw new ArgumentException("別の型とは比較できません。", "obj");
            }

            return this.Title.CompareTo(((ModData)obj).Title);
        }
    }
}
