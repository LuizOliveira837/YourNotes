namespace YourNotes.Communication.Requests.Topic
{
    public class RequestUpdateTopicJson
    {
        public Guid Id { get; set; }
        public string NewTitle { get; set; } = string.Empty;
        public string NewDescription { get; set; } = string.Empty;
    }
}
