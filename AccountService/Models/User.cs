namespace AccountService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;


        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public ICollection<User> Subscriptions { get; set; } = null!;
        public ICollection<User> Subscribers { get; set; } = null!;

        public ICollection<Notification> Notifications { get; set; } = null!;
    }
}
