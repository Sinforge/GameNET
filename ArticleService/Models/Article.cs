using System.ComponentModel.DataAnnotations;

namespace GameHub.Models
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
        public string OwnerId { get; set; } = null!;
    }
}
