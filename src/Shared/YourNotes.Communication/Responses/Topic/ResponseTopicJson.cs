namespace YourNotes.Communication.Responses.Topic
{
    public class ResponseTopicJson
    {
        public ResponseTopicJson(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
