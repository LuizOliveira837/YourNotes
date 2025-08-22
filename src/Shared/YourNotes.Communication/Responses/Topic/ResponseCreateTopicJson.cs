namespace YourNotes.Communication.Responses.Topic
{
    public class ResponseCreateTopicJson
    {
        public ResponseCreateTopicJson(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
