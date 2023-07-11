using System.ComponentModel.DataAnnotations;

namespace ChatService.Models
{
    public class ChatInfo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual Guid GameId { get; set; }
        public ChatInfo(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }
        public ChatInfo() { }
    }
}
