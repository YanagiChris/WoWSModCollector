using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WoWSModCollector.ModClass;

namespace WoWSModCollector
{
    class XmlManager
    {

        private string Xmlpath;

        public XmlManager(string argXmlPath) 
        {
            Xmlpath = argXmlPath;
            //設定ファイル確認
            if (File.Exists(Xmlpath) == false)
            {
                MyMods myMods = new MyMods();
                XmlWriter(myMods);
                
            }
        }

        public void XmlWriter(MyMods myMods)
        {
            //設定ファイルが無かったら作る
            if (File.Exists(Xmlpath) == false)
            {
                File.Create(Xmlpath).Close();
            }

            //＜XMLファイルに書き込む＞
            //XmlSerializerオブジェクトを作成
            //書き込むオブジェクトの型を指定する
            XmlSerializer serializer = new XmlSerializer(typeof(MyMods));
            //ファイルを開く（UTF-8 BOM無し）
            StreamWriter sw = new StreamWriter(Xmlpath, false, new UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, myMods);
            //閉じる
            sw.Close();
        }

        public MyMods XmlReader()
        {
            //＜XMLファイルから読み込む＞
            //XmlSerializerオブジェクトの作成
            XmlSerializer serializer = new XmlSerializer(typeof(MyMods));
            //ファイルを開く
            StreamReader sr = new StreamReader(
                Xmlpath, new System.Text.UTF8Encoding(false));
            //XMLファイルから読み込み、逆シリアル化する
            MyMods myMods = (MyMods)serializer.Deserialize(sr);
            //閉じる
            sr.Close();

            return myMods;
        }

    }
}
