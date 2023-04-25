namespace AccountService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public bool isChecked { get; set; }

        public User User { get; set; } = null!;

    }
}
