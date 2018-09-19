using System.IO;
using System.Xml.Serialization;

namespace FoxCoreUtility.Files.Serializer
{
    public class SerializerXml<T> : IGeneralAccess<T> where T:class 
    {
        public void Store(string filePath, T obj)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            if (null == obj) return;
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, obj);
            }
        }


        public T Load(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(fs);
            }
        }


        public static void Store(string filePath, T sourceObj, string xmlRootName)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                using (var writer = new StreamWriter(filePath))
                {
                    XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new XmlSerializer(typeof(T)) :
                        new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));

                    //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //ns.Add("", "");

                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public static T Load(string filePath, string xmlRootName)
        {
            T result = default(T);
            try
            {
                if (File.Exists(filePath))
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new XmlSerializer(typeof(T)) :
                        new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));

                        result = (T)xmlSerializer.Deserialize(reader);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return result;
        }
    }


}
