using ArticleService.Models;

namespace ArticleService.DTOs
{
    public class CommentCreateDto
    {
        public int? ParentId { get; set; } = null!;
        public DateTime Date { get; set; }

        public string Content { get; set; } = null!;

        public string Owner { get; set; } = null!;

        public int ArticleId { get; set; }
    }
}
