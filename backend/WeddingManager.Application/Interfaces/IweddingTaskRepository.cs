using WeddingManager.Domain.Entities;

namespace WeddingManager.Application.Interfaces
{
    public interface IWeddingTaskRepository
    {
        Task<WeddingTask> AddAsync(WeddingTask weddingTask);

        Task<IEnumerable<WeddingTask>> GetTasksByWeddingIdAsync(Guid weddingId);

        Task<WeddingTask> GetByIdAsync(Guid id, Guid weddingId);

        Task<WeddingTask> UpdateAsync(WeddingTask weddingTask);

        Task<Guid> DeleteAsync(Guid id);
    }
}