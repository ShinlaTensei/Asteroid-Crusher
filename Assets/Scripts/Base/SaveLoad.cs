using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Base
{
    public static class SaveLoad
    {
        public static void SaveToBinary(object data, string fileName)
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, 
                FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static List<ShipInfo> LoadShipInfo(string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                List<ShipInfo> info = formatter.Deserialize(stream) as List<ShipInfo>;
                stream.Close();
                return info;
            }
            return null;
        }

        public static void LoadShipInfo(out List<ShipInfo> info, string fileName)
        {
            info = LoadShipInfo(fileName);
        }
    }
}

