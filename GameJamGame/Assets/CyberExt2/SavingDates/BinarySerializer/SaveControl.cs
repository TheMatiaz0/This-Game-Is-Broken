using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
#if UNITY_STANDALONE
using UnityEngine;
#endif
namespace Cyberevolver
{
    
  
    public static class SaveControl
    {
#if UNITY_STANDALONE
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            SetPath(System.IO.Path.Combine(Application.dataPath, "Saves"));
        }
#endif

        private static BinaryFormatter BinaryFormatter { get; } = new BinaryFormatter();
        private const string enlargement = "";
        private static string _Path;
        public static string Path
        {
            get => _Path;
            set
            {
                _Path = value;
                Directory.CreateDirectory(_Path);
            }
        }
        public static void SetPath(string to)
        {
            Path = to;
            Directory.CreateDirectory(Path);
        }
        public static bool Exist<T>(string fileName) => File.Exists(FullPath<T>(fileName));
        public static void SaveObject<T>(T toSave, string fileName, string customPath = null)
        {
            TestToAtribute<T>();
            using (FileStream file = new FileStream(FullPath<T>(fileName, customPath), FileMode.Create))
            {
                BinaryFormatter.Serialize(file, toSave);
            }
        }
        public static T TryLoad<T>(string fileName, string customPath = null)
             where T : class
        {
            try
            {
                return LoadObject<T>(fileName, customPath);
            }
            catch
            {
                return null;
            }
        }
        public static T LoadObject<T>(string fileName, string customPath = null)
        {
            TestToAtribute<T>();
            string fullPath = FullPath<T>(fileName, customPath);
            try
            {
                FileStream file = new FileStream(fullPath, FileMode.Open);
                T loadedObject = (T)BinaryFormatter.Deserialize(file);
                file.Close();
                return loadedObject;
            }
            catch (Exception e)
            {
                throw new SerializationException($"File has not been found: {e.Message}");
            }

        }
        private static string FullPath<T>(string fileName, string customPath = null) => System.IO.Path.Combine(customPath ?? Path, $"{fileName}.{enlargement}{typeof(T).Name}");
        private static void TestToAtribute<T>()
        {
            if (typeof(T).IsSerializable == false)
            {
                throw new SerializationException($"[Serializable] attribute is missing");
            }
        }
    }
}



