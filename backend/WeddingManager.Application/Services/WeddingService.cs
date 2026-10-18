using WeddingManager.Domain.Entities;
using WeddingManager.Application.Interfaces;
using WeddingManager.Application.DTOs;



namespace WeddingManager.Application.Services
{
    public class WeddingService
    {
        private readonly IWeddingRepository _weddingRepository;

        public WeddingService(IWeddingRepository weddingRepository)
        {
            _weddingRepository = weddingRepository;
        }

        public async Task<Guid> CreateWeddingAsync(CreateWeddingDto createWeddingDto)
        {
            var wedding = new Wedding
            {
                Id = Guid.NewGuid(),
                Name = createWeddingDto.Name,
                WeddingDate = createWeddingDto.WeddingDate,
                Location = createWeddingDto.Location
            };

            await _weddingRepository.AddAsync(wedding);

            return wedding.Id;
        }

        public async Task<Wedding> GetWeddingByIdAsync(Guid id)
        {
            return await _weddingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Wedding>> GetAllWeddingsAsync()
        {
            return await _weddingRepository.GetAllAsync();
        }

        public async Task UpdateWeddingAsync(Guid id, UpdateWeddingDto updateWeddingDto)
        {
            var wedding = await _weddingRepository.GetByIdAsync(id);

            if (wedding == null)
            {
                throw new Exception("Wedding not found");
            }

            wedding.Name = updateWeddingDto.Name;
            wedding.WeddingDate = updateWeddingDto.WeddingDate;
            wedding.Location = updateWeddingDto.Location;

            await _weddingRepository.UpdateAsync(wedding);
        }

        public async Task DeleteWeddingAsync(Guid id)
        {
            var wedding = await _weddingRepository.GetByIdAsync(id);

            if (wedding == null)
            {
                throw new Exception("Wedding not found");
            }

            await _weddingRepository.DeleteAsync(wedding);
        }

    }
}