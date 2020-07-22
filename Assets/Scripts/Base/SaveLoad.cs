﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

namespace Base
{
    public static class SaveLoad
    {
        public static void SaveToBinary(object data, string fileName)
        {
            GameManager.Instance.Log("Vào SaveLoad.SaveToBinary");
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, 
                FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, data);
            }
            catch (Exception e)
            {
                GameManager.Instance.Log("Vào SaveLoad.SaveToBinary: " + e.Message);
                throw;
            }

            stream.Close();
        }

        public static void LoadFromBinary<T>(out T result, string filename)
        {
            GameManager.Instance.Log("Vào SaveLoad.LoadFromBinary");
            string path = Application.persistentDataPath + "/" + filename;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                result = (T) formatter.Deserialize(stream);
                stream.Close();
            }
            else result = default;
        }

        public static void SaveToJson(object data, string filename)
        {
            GameManager.Instance.Log("Vào SaveLoad.SaveToJson");
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);
            try
            {
                string json = JsonUtility.ToJson(data);
                sw.WriteLine(json);
            }
            catch (Exception e)
            {
                GameManager.Instance.Log("Vào SaveLoad.LoadFromJson: " + e.Message);
                throw;
            }
            sw.Close();
            stream.Close();
        }

        public static void LoadFromJson<T>(out T result, string filename)
        {
            GameManager.Instance.Log("Vào SaveLoad.LoadFromJson");
            string path = Application.persistentDataPath + "/" + filename;
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string jsonStr = reader.ReadLine();
                result = JsonUtility.FromJson<T>(jsonStr);
                reader.Close();
                stream.Close();
            }
            else result = default;
        }
    }
}

