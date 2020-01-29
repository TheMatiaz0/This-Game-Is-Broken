using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cyberevolver
{
    public class SerializeXMl<T>
    {

#if UNITY_STANDALONE
		/// <summary>
		/// Runs on start of application. Sets PathToSpecificFolder property.
		/// </summary>
		/// <param name="folderName"></param>
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        public static void Init(string folderName = "Saves")
        {
            PathToSpecificFolder = System.IO.Path.Combine(UnityEngine.Application.dataPath, folderName);   
        }
#endif
        private static string PathToSpecificFolder { get; set; }

		/// <summary>
		/// Get absolute path connected with <c>fileName</c> parameter.
		/// </summary>
		/// <param name="fileName">fileName is name of XML file</param>
		/// <returns>Absolute path</returns>
        public static string GetPath(string fileName)
        {
            return System.IO.Path.Combine(PathToSpecificFolder, fileName);
        }

		/// <summary>
		/// Serialize generic type item with XML providing fileName of XML final file and it's extension. 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="fileName"></param>
		/// <param name="ownExtension"></param>
        public static void SerializeXml(T item, string fileName, string ownExtension = "xml")
        {
            Directory.CreateDirectory(PathToSpecificFolder);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(GetPath($"{fileName}.{ownExtension}")))
            {
                serializer.Serialize(writer, item);
            }          
        }

		/// <summary>
		/// Load serialized XML data providing simple fileName of XML previously created file and it's extension.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="ownExtension"></param>
		/// <returns></returns>
        public static T LoadXml(string fileName, string ownExtension = "xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(GetPath($"{fileName}.{ownExtension}")))
            {
                return (T)serializer.Deserialize(reader);
            }
             
        }
    }
}

