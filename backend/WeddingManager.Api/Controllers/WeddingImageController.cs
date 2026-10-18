using Microsoft.AspNetCore.Mvc;
using WeddingManager.Application.Interfaces;

namespace WeddingManager.Api.Controllers
{
    [ApiController]
    [Route("api/weddings/{weddingId}/images")]
    public class WeddingImageController: ControllerBase
    {
        private readonly IFileStorage _fileStorage;

        public WeddingImageController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(Guid weddingId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was provided.");
            }

            await using var stream = file.OpenReadStream();

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var key = $"weddings/{weddingId}/images/{uniqueFileName}";
            var contentType = file.ContentType;

            var result = await _fileStorage.UploadAsync(stream, key, contentType);

            return Ok   (new { FileName = result });
        }
    }
}

