
namespace KT.Utility.Files.Serializer
{
    public interface IGeneralAccess<T>
    {
        void Store(string filePath, T obj);
        T Load(string filePath);
    }
}
