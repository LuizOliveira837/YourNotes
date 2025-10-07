namespace YourNotes.Domain.Entities
{
    public class Content : BaseEntity
    {
        public Content(Guid articleId, string markup, int position)
        {
            ArticleId = articleId;
            Markup = markup;
            Position = position;
        }

        public Guid ArticleId { get; set; }
        public string Markup { get; set; }  = string.Empty;
        public int Position { get; set; } 
    }
}
