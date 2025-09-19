namespace YourNotes.Communication.Requests.Article
{
    public class RequestArticleJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid TopicId { get; set; } = Guid.Empty;
    }
}
