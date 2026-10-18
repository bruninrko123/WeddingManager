using WeddingManager.Domain.Entities;
using WeddingManager.Application.Interfaces;
using WeddingManager.Application.DTOs;

namespace WeddingManager.Application.Services
{
    public class WeddingTaskService
    {
        private readonly IWeddingTaskRepository _weddingTaskRepository;
        private readonly IWeddingRepository _weddingRepository;

        public WeddingTaskService(IWeddingTaskRepository weddingTaskRepository, IWeddingRepository weddingRepository)
        {
            _weddingTaskRepository = weddingTaskRepository;
            _weddingRepository = weddingRepository;
        }

        public async Task<WeddingTask> CreateWeddingTaskAsync(Guid weddingId, CreateWeddingTaskDto createWeddingTaskDto)
        {
            var wedding = await _weddingRepository.GetByIdAsync(weddingId);

            if (wedding == null)
            {
                throw new Exception("Wedding not found");
            }

            var weddingTask = new WeddingTask
            {
                Id = Guid.NewGuid(),
                WeddingId = weddingId,
                Title = createWeddingTaskDto.Title,
                Description = createWeddingTaskDto.Description,
                DueDate = createWeddingTaskDto.DueDate
            };

            await _weddingTaskRepository.AddAsync(weddingTask);
            return weddingTask;
        }

        public async Task<IEnumerable<WeddingTask>> GetTasksByWeddingIdAsync(Guid weddingId)
        {
            return await _weddingTaskRepository.GetTasksByWeddingIdAsync(weddingId);
        }

        public async Task<WeddingTask> GetSingleWeddingTaskByIdAsync(Guid id, Guid weddingId)
        {
            return await _weddingTaskRepository.GetByIdAsync(id, weddingId);
        }

        public async Task<WeddingTask> UpdateWeddingTaskAsync(Guid id, Guid weddingId, UpdateWeddingTaskDto updateWeddingTaskDto)
        {
            var weddingTask = await _weddingTaskRepository.GetByIdAsync(id, weddingId);

            if (weddingTask == null)
            {
                throw new Exception("Wedding task not found");
            }

            weddingTask.Title = updateWeddingTaskDto.Title;
            weddingTask.Description = updateWeddingTaskDto.Description;
            weddingTask.DueDate = updateWeddingTaskDto.DueDate;
            weddingTask.IsCompleted = updateWeddingTaskDto.IsCompleted;

            return await _weddingTaskRepository.UpdateAsync(weddingTask);
        }

        public async Task<Guid> DeleteWeddingTaskAsync(Guid id)
        {
            return await _weddingTaskRepository.DeleteAsync(id);
        }
        

    }
}