using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FoxCoreUtility.Files.Serializer
{
    public class SerializerBinary<T> : IGeneralAccess<T> where T : class 
    {

        #region Implementation of IGeneralAccess<T>

        public void Store(string filePath, T obj)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            if (null == obj) return;
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }

        public T Load(string filePath)
        {
            if (!File.Exists(filePath)) return default(T);
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }

        #endregion
    }
}
