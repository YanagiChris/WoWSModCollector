using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WoWSModCollector
{
    public class ModData : System.IComparable
    {
        public string Title;
        public string ThumbFileName;
        public List<string> fileList;

        public ModData()
        {
            Title = "";
            ThumbFileName = "";
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
