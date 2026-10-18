namespace WeddingManager.Application.DTOs
{
    public class CreateWeddingDto
    {
        public string Name { get; set; } = "";
        public DateOnly WeddingDate { get; set; }
        public string Location { get; set; } = "";
    }
}