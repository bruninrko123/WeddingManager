namespace WeddingManager.Domain.Entities
{
    public class WeddingUser
    {
        public Guid UserId { get; set; }
        public Guid WeddingId { get; set; }
        public User User { get; set; } = null!;
        public Wedding Wedding { get; set; } = null!;


    }
}