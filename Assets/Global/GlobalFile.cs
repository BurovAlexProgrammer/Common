using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Object = System.Object;

namespace Global.File
{
    public static class GlobalFile
    {
        public static void SaveFile(this Object data, string path)
        {
            using FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(file, data);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

        public static T LoadFile<T>(string path) where T : new()
        {
            T result = new T();
            
            using FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                result = (T)formatter.Deserialize(file);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }

            return result;
        }
    }
}