namespace WeddingManager.Domain.Entities
{
    public class WeddingTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public DateOnly DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public Guid WeddingId { get; set; }

        //navigation property
        public Wedding Wedding { get; set; } = null!;    


    }
}