using WeddingManager.Domain.Entities;

namespace WeddingManager.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> UploadAsync(Stream file, string fileName, string contentType);
    }
}