using System.ComponentModel.DataAnnotations.Schema;

namespace YourNotes.Domain.Entities
{
    [Table("Topics")]
    public class Topic : BaseEntity
    {
        public Topic(Guid userId, string title)
        {
            UserId = userId;
            Title = title;
        }

        public Topic()
        {
            
        }

        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;


    }
}
