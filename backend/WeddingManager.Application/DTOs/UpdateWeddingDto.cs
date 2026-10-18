namespace WeddingManager.Application.DTOs
{
    public class UpdateWeddingDto
    {
        public string Name { get; set; } = "";
        public DateOnly WeddingDate { get; set; }

        public string Location { get; set; } = "";

        
    }
}