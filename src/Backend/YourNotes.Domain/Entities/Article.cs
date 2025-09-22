namespace YourNotes.Domain.Entities
{
    public class Article : BaseEntity
    {
        public Article(string title, string description, bool publishOn)
        {
            Title = title;
            Description = description;
            PublishOn = publishOn;
        }

        public Article()
        {

        }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool PublishOn { get; set; } = false;

        public Guid TopicId { get; set; }
    }
}
