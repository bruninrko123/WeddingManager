using Microsoft.AspNetCore.Mvc;
using WeddingManager.Application.DTOs;
using WeddingManager.Application.Services;


namespace WeddingManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeddingController : ControllerBase
    {
        private readonly WeddingService _weddingService;

        public WeddingController(WeddingService weddingService)
        {
            _weddingService = weddingService;
        }

        [HttpPost("CreateWedding")]
        public async Task<IActionResult> CreateWedding(CreateWeddingDto createWeddingDto)
        {
            var id = await _weddingService.CreateWeddingAsync(createWeddingDto);

            return CreatedAtAction(nameof(GetWeddingById), new { id }, new { id });
        }

        [HttpGet("GetWeddingById/{id}")]
        public async Task<IActionResult> GetWeddingById(Guid id)
        {
            try
            {


                var wedding = await _weddingService.GetWeddingByIdAsync(id);

                if (wedding == null)
                {
                    return NotFound();
                }

                return Ok(new WeddingResponseDto
                {
                    Id = wedding.Id,
                    Name = wedding.Name,
                    WeddingDate = wedding.WeddingDate,
                    Location = wedding.Location
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while retrieving the wedding: {ex.Message}");
            }
        }

        [HttpGet("GetAllWeddings")]
        public async Task<IActionResult> GetAllWeddings()
        {
            try
            {


                var weddings = await _weddingService.GetAllWeddingsAsync();

                var response = weddings.Select(w => new WeddingResponseDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    WeddingDate = w.WeddingDate,
                    Location = w.Location
                });

                return Ok(response);
            } catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while retrieving weddings: {ex.Message}");
            }
        }

        [HttpPut("UpdateWedding/{id}")]
        public async Task<IActionResult> UpdateWedding(Guid id, UpdateWeddingDto updateWeddingDto)
        {
            try

            {
                await _weddingService.UpdateWeddingAsync(id, updateWeddingDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while updating the wedding: {ex.Message}");
            }
        }
    }
}