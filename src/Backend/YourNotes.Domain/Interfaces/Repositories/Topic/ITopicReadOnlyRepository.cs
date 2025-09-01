namespace YourNotes.Domain.Interfaces.Repositories.Topic
{
    public interface ITopicReadOnlyRepository
    {
        public Task<bool> TopicAlreadyExists(string title, Guid id);
        public Task<Domain.Entities.Topic?> GetTopicByIdAndUserId(Guid id, Guid userId);
        public Task<IList<Domain.Entities.Topic>> GetTopics(Domain.Entities.User user);
    }
}
