namespace GameHub.DTOs
{
    public class ArticleCreateDto
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        public string Owner { get; set; } = null!;
    }
}
