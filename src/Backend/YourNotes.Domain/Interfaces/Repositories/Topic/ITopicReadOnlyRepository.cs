namespace YourNotes.Domain.Interfaces.Repositories.Topic
{
    public interface ITopicReadOnlyRepository
    {
        public Task<bool> TopicAlreadyExists(string title, Guid id);
    }
}
