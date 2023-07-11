using System.ComponentModel.DataAnnotations;

namespace ChatService.Models
{
    public class GameInfo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        // shooter, fps, moba, etc.
        public virtual ICollection<string> Tags { get; set; }
        // All chat of this game

        public GameInfo() { }
        public GameInfo(string name, ICollection<string> tags)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tags = tags;
        }
    }
}
