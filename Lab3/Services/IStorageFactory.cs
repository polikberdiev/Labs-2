using Lab2.BL.Services;

namespace Lab3.Services
{
    public interface IStorageFactory
    {
        IStorage<T> CreateFileStorageViewModel<T>(string filePath) where T : new();
    }
}