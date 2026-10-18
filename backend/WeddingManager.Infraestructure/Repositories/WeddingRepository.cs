using WeddingManager.Domain.Entities;
using WeddingManager.Application.Interfaces;
using WeddingManager.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace WeddingManager.Infraestructure.Repositories
{
    public class WeddingRepository : IWeddingRepository
    {
        private readonly AppDbContext _context;

        public WeddingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Wedding> AddAsync(Wedding wedding)
        {
            _context.Weddings.Add(wedding);
            await _context.SaveChangesAsync();

            return wedding;
        }

        public async Task<Wedding> GetByIdAsync(Guid id)
        {

            return await _context.Weddings.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Wedding>> GetAllAsync()
        {
            return await _context.Weddings.ToListAsync();
        }

        public async Task<Wedding> UpdateAsync(Wedding wedding)
        {
            _context.Weddings.Update(wedding);

            await _context.SaveChangesAsync();

            return wedding;
        }
        
        public async Task DeleteAsync(Wedding wedding)
        {
            _context.Weddings.Remove(wedding);
            
            await _context.SaveChangesAsync();
        }

    }
}