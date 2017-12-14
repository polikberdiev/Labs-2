using Lab2.BL.Services;
using Lab3.Models;

namespace Lab3.Services
{
    public class StorageFactory : IStorageFactory
    {
        public IStorage<T> CreateFileStorageViewModel<T>(string filePath)
            where T : new()
        {
            return new Storage<T>(new FileStorageIO<T>(filePath));
        }
    }
}