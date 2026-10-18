namespace WeddingManager.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        public ICollection<WeddingUser> WeddingUsers { get; set; } = new List<WeddingUser>();

    }
}