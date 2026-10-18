namespace WeddingManager.Domain.Entities
{
    public class Wedding
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = "";
        public DateOnly WeddingDate { get; set; }
        public string Location { get; set; } = "";


        //Collection for WeddingTasks
        public ICollection<WeddingTask> WeddingTasks { get; set; } = new List<WeddingTask>();

        public ICollection<WeddingUser> WeddingUsers { get; set; } = new List<WeddingUser>();

    }
}