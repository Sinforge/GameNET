namespace GameHub.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        public User? Owner { get; set; } = null!;
    }
}
