using ArticleService.Models;

namespace ArticleService.DTOs
{
    public class CommentCreateDto
    {
        public int FatherCommentId { get; set; }
        public string Date { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string Owner { get; set; } = null!;

        public int ArticleId { get; set; }
    }
}
