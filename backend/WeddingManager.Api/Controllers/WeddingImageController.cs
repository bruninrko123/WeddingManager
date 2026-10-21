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

        public async Task<bool> KeyBelongsToWedding(string key, Guid weddingId)
        {
            var weddingImagePrefix = $"weddings/{weddingId}/images/";
            if (!key.StartsWith(weddingImagePrefix, StringComparison.Ordinal) ||
                key.Length == weddingImagePrefix.Length)
            {
                return false;
            }
            return true;
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

            return Ok(new { FileName = result });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid weddingId, [FromQuery] string key, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was provided.");
            }

            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Key cannot be null or empty.");
            }

            if (!await KeyBelongsToWedding(key, weddingId))
            {
                return BadRequest("The image does not belong to this wedding.");
            }

            await using var stream = file.OpenReadStream();
            var contentType = file.ContentType;

            var result = await _fileStorage.UpdateAsync(stream, key, contentType);

            return Ok(new { FileName = result });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid weddingId, [FromQuery] string key)
        {
            //validate that this image belongs to this weddingId, if not return BadRequest
            if (!await KeyBelongsToWedding(key, weddingId))
            {
                return BadRequest("The image does not belong to this wedding.");
            }

            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Key cannot be null or empty.");
            }

            await _fileStorage.DeleteAsync(key);
            return Ok();
        }
    }
}

