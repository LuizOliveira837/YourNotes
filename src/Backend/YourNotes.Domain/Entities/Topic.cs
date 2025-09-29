using System.ComponentModel.DataAnnotations.Schema;

namespace YourNotes.Domain.Entities
{
    [Table("Topics")]
    public class Topic : BaseEntity
    {
        public Topic(Guid userId, string title, string description)
        {
            UserId = userId;
            Title = title;
            Description = description;
        }

        public Topic()
        {
            
        }

        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


    }
}
