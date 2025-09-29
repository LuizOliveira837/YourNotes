namespace YourNotes.Communication.Responses.Topic
{
    public class ResponseTopicJson
    {
        public ResponseTopicJson(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
