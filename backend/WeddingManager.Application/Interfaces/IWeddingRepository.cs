using WeddingManager.Domain.Entities;

namespace WeddingManager.Application.Interfaces
{
    public interface IWeddingRepository
    {
        Task<Wedding> AddAsync(Wedding wedding);

        Task<Wedding> GetByIdAsync(Guid id);

        Task<IEnumerable<Wedding>> GetAllAsync();

        Task<Wedding> UpdateAsync(Wedding wedding);

        Task DeleteAsync(Wedding wedding);
    }

    
}
