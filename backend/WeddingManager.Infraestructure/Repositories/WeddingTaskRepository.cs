using WeddingManager.Domain.Entities;
using WeddingManager.Application.Interfaces;
using WeddingManager.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WeddingManager.Infraestructure.Repositories
{
    public class WeddingTaskRepository: IWeddingTaskRepository
    {
        private readonly AppDbContext _context;

        public WeddingTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WeddingTask> AddAsync(WeddingTask weddingTask)
        {
            _context.WeddingTasks.Add(weddingTask);
            await _context.SaveChangesAsync();

            return weddingTask;
        }

        public async Task<IEnumerable<WeddingTask>> GetTasksByWeddingIdAsync(Guid weddingId)
        {
            return await _context.WeddingTasks
                .AsNoTracking()
                .Where(w => w.WeddingId == weddingId)
                .Select(w => new WeddingTask
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    DueDate = w.DueDate,
                    IsCompleted = w.IsCompleted,
                    WeddingId = w.WeddingId
                })
                .ToListAsync();
        }

        public async Task<WeddingTask> GetByIdAsync(Guid id, Guid weddingId)
        {
            return await _context.WeddingTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.WeddingId == weddingId);
        }

        public async Task<WeddingTask> UpdateAsync(WeddingTask weddingTask)
        {
            _context.WeddingTasks.Update(weddingTask);
            await _context.SaveChangesAsync();
            return weddingTask;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var weddingTask = await _context.WeddingTasks.FindAsync(id);
            if (weddingTask == null)
            {
                throw new Exception("Wedding task not found");
            }

            _context.WeddingTasks.Remove(weddingTask);

            await _context.SaveChangesAsync();
            return id;
        }
        


    }
}