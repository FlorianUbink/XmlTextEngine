using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlFormEngine
{
    public class Gate
    {
        public static void game_Safe(Data data, string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Data));
            string path = @Program.xSettings.Root.Element("Path").Element("Saves").Value + "\\" + fileName;
            TextWriter writer = new StreamWriter(path);
            xs.Serialize(writer, data);
            writer.Close();
        }

        public static Data game_Load(string fileName)
        {
            
            if (File.Exists(fileName))
            {

                XmlSerializer xs = new XmlSerializer(typeof(Data));
                FileStream read = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                return (Data)xs.Deserialize(read);
            }
            else
                return null;
        }
    }
}
