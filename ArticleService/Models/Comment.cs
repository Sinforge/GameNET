using System.ComponentModel.DataAnnotations;

namespace ArticleService.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int TotalRep { get; set; } = 0;
        [Required]
        public string Date { get; set; } =  null!;
        [Required]
        public string Content { get; set; } = null!;

        public virtual ICollection<Comment> Replies { get; set; }
        [Required]
        public string Owner { get; set; } = null!;

 
        public Article Article { get; set; } = null!;
    }
}
