namespace YourNotes.Domain.Interfaces.Repositories.Topic
{
    public interface ITopicWriteOnlyRepository
    {
        public Task<Guid> CreateAsync(YourNotes.Domain.Entities.Topic topic);
        public void Update(YourNotes.Domain.Entities.Topic topic);
    }
}
