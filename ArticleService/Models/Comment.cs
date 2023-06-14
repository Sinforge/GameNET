using System.ComponentModel.DataAnnotations;

namespace ArticleService.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int Reputation { get; set; } = 0;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string Owner { get; set; } = null!;
        [Required]
        public int ArticleId { get; set; }
        public int? ParentId { get; set; } = null;

    }
}
