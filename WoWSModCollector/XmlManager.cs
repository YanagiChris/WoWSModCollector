//Copyright (c) 2020 Yanagi Chris twitter@YanagiChris
//This software is released under the MIT License.
//http://opensource.org/licenses/mit-license.php

using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using WoWSModCollector.ModClass;

namespace WoWSModCollector
{
    class XmlManager
    {

        private string XmlPath;

        public XmlManager(string argXmlPath) 
        {
            XmlPath = argXmlPath;

            if (CheckSettingFile() == false)
            {
                return;
            }

        }

        public bool CheckSettingFile()
        {
            //設定ファイルが無かったら作る
            if (File.Exists(XmlPath) == false)
            {
                File.Create(XmlPath).Close();
                MyMods myMods = new MyMods();
                XmlWriter(myMods);
            }

            try
            {
                using (Stream stream = new FileStream(XmlPath, FileMode.Open)) { }
            }
            catch
            {
                MessageBox.Show("Another process is using a setting file.");

                return false;
            }

            return true;
        }

        public void XmlWriter(MyMods myMods)
        {

            //＜XMLファイルに書き込む＞
            //XmlSerializerオブジェクトを作成
            //書き込むオブジェクトの型を指定する
            XmlSerializer serializer = new XmlSerializer(typeof(MyMods));
            //ファイルを開く（UTF-8 BOM無し）

            try
            {
                StreamWriter sw = new StreamWriter(XmlPath, false, new UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, myMods);
                //閉じる
                sw.Close();
            }
            catch
            {
                MessageBox.Show("Failed to write a setting file. Another process is using a setting file.");
                return;
            }
            
        }

        public MyMods XmlReader()
        {
            //＜XMLファイルから読み込む＞
            //XmlSerializerオブジェクトの作成
            XmlSerializer serializer = new XmlSerializer(typeof(MyMods));
            //ファイルを開く
            StreamReader sr = new StreamReader(
                XmlPath, new System.Text.UTF8Encoding(false));
            //XMLファイルから読み込み、逆シリアル化する
            MyMods myMods = (MyMods)serializer.Deserialize(sr);
            //閉じる
            sr.Close();

            return myMods;
        }

    }
}
