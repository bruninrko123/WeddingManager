namespace WeddingManager.Application.DTOs
{
    public class UpdateWeddingTaskDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateOnly DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}