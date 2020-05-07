using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RockyHockey.Common
{
    /// <summary>
    /// Has the functionality to serialize an object to a file
    /// </summary>
    public static class ObjectSerializer
    {
        /// <summary>
        /// Serializes the surpassed object into a file
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="serializableObject">object to serialize</param>
        /// <param name="fileName">name of the file to write to</param>
        /// <returns>executeable Task</returns>
        public static void SerializeObject<T>(T serializableObject, string fileName = "")
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(typeof(T));
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Seserializes a object from a file and returns the instance of this object
        /// </summary>
        /// <typeparam name="T">type of the deserialized object</typeparam>
        /// <param name="fileName">name of the file to read from</param>
        /// <returns>instance of the deserialized object</returns>
        public static T DeserializeObject<T>(string fileName = "")
        {
            T objectOut = default(T);

            if (!string.IsNullOrEmpty(fileName))
            {
                if (!File.Exists(fileName))
                {
                    SerializeObject(default(T));
                }
                else
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(fileName);
                    string xmlString = xmlDocument.OuterXml;

                    using (StringReader read = new StringReader(xmlString))
                    {
                        Type outType = typeof(T);

                        XmlSerializer serializer = new XmlSerializer(outType);
                        using (XmlReader reader = new XmlTextReader(read))
                        {
                            objectOut = (T)serializer.Deserialize(reader);
                            reader.Close();
                        }

                        read.Close();
                    }
                }
            }

            return objectOut;
        }
    }
}
