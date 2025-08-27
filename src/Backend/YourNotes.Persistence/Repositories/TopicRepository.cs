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
                _context
                .Topics
                .AnyAsync(t => t.Title == title && t.UserId == id);
        }
    }
}
