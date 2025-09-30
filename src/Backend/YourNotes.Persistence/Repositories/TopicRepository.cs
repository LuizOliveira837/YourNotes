using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class TopicRepository : ITopicReadOnlyRepository, ITopicWriteOnlyRepository
    {
        private readonly YourNotesDbContext _context;

        public TopicRepository(YourNotesDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(Topic topic)
        {
            await _context
                 .Topics
                 .AddAsync(topic);

            return topic.Id;
        }

        async Task<Topic?> ITopicReadOnlyRepository.GetTopicByIdAndUserId(Guid id, Guid userId)
        {
            var query = await GetFullTopics()
                .AsNoTracking()
                .Where(t => t.Id == id && t.UserId == userId && t.Active)
                .FirstOrDefaultAsync();

            return query;
        }

        async Task<Topic?> ITopicWriteOnlyRepository.GetTopicByIdAndUserId(Guid id, Guid userId)
        {
            var query = await GetFullTopics()
                .Where(t => t.Id == id && t.UserId == userId && t.Active)
                .FirstOrDefaultAsync();

            return query;
        }


        public async Task<IList<Topic>> GetTopics(User user)
        {
            return await
                  _context
                 .Topics
                 .AsNoTracking()
                 .Where(t => t.UserId == user.Id && t.Active)
                 .ToListAsync();
        }

        public async Task<bool> TopicAlreadyExists(string title, Guid id)
        {
            return await
                GetFullTopics()
                .AsNoTracking()
                .AnyAsync(t => t.Title == title && t.UserId == id);
        }


        public IQueryable<Topic> GetFullTopics()
        {
            return _context
                .Topics
                .AsQueryable();
        }

        public async Task<bool> TopicExists(Guid topicId, Guid Userid)
        {
            return await
                _context
                .Topics
                .AsNoTracking()
                .AnyAsync(t => t.Id == topicId && t.UserId == Userid && t.Active);
        }

        public void Update(Topic topic) => _context.Topics.Update(topic);

        public void Delete(Topic topic)
        {
            _context
               .Topics
               .Remove(topic);
        }
    }
}
