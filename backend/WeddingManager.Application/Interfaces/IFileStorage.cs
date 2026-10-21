using WeddingManager.Domain.Entities;

namespace WeddingManager.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> UploadAsync(Stream file, string key, string contentType);

        Task<string> UpdateAsync(Stream file, string key, string contentType);

        Task DeleteAsync(string key);

        
    }
}