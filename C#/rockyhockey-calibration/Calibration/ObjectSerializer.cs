using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Calibration
{
    public class ObjectSerializer
    {
            /// <summary>
            /// Creates a new instance of the ObjectSerializer, that serializes the objects into the file "fileName"
            /// </summary>
            /// <param name="fileName">name of the file with the serialized objects</param>
            public ObjectSerializer(string fileName)
            {
                this.fileName = fileName;
            }

            private string fileName;

            /// <summary>
            /// Serializes the surpassed object into a file
            /// </summary>
            /// <typeparam name="T">Type of the object to serialize</typeparam>
            /// <param name="serializableObject">object to serialize</param>
            /// <returns>executeable Task</returns>
            public void SerializeObject<T>(T serializableObject)
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

            /// <summary>
            /// Seserializes a object from a file and returns the instance of this object
            /// </summary>
            /// <typeparam name="T">type of the deserialized object</typeparam>
            /// <returns>instance of the deserialized object</returns>
            public T DeSerializeObject<T>()
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return default(T);
                }
                else if (!File.Exists(fileName))
                {
                    SerializeObject(default(T));
                }

                T objectOut = default(T);
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
                return objectOut;
            }
        }
    

}