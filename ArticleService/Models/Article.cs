using System.ComponentModel.DataAnnotations;

namespace ArticleService.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public string Owner { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; } 
    }
}
