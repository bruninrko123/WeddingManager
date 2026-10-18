namespace WeddingManager.Application.DTOs
{
    public class WeddingResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateOnly WeddingDate { get; set; }
        public string Location { get; set; } = "";
    }
}