namespace WeddingManager.Application.DTOs
{
    public class CreateWeddingTaskDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public DateOnly DueDate { get; set; }

    }

}