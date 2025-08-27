using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.Topic;

namespace CommonTestUtilities.Builders
{
    public class TopicReadOnlyRepositoryBuilder
    {
        public Mock<ITopicReadOnlyRepository> repository;

        public TopicReadOnlyRepositoryBuilder(string? title, YourNotes.Domain.Entities.User? user)
        {
            repository = new();

            if (!string.IsNullOrEmpty(title) && user is not null)
            {
                TopicAlreadyExists(title, user.Id);


            }

            GetTopics(title, user);
        }

        public void TopicAlreadyExists(string title, Guid id)
        {
            repository
                .Setup(t => t.TopicAlreadyExists(title, id))
                .ReturnsAsync(true);

        }

        public void GetTopics(string? title, YourNotes.Domain.Entities.User? user)
        {
            IList<Topic> topics = new List<Topic>();

            if (!string.IsNullOrEmpty(title) && user is not null)
            {
                topics.Add(new Topic(user!.Id, title!));

            }

            repository
                  .Setup(t => t.GetTopics(user))
                  .ReturnsAsync(topics);
        }

    }
}
