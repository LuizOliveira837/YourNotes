using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.Topic;

namespace CommonTestUtilities.Builders
{
    public class TopicReadOnlyRepositoryBuilder
    {
        public Mock<ITopicReadOnlyRepository> repository;

        public TopicReadOnlyRepositoryBuilder(YourNotes.Domain.Entities.User? user, string? title = "", string? description = "")
        {
            repository = new();

            if (!string.IsNullOrEmpty(title) && user is not null)
            {
                TopicAlreadyExists(title, user.Id);

            }

            GetTopics(description, title, user);
        }

        public void TopicAlreadyExists(string title, Guid id)
        {
            repository
                .Setup(t => t.TopicAlreadyExists(title, id))
                .ReturnsAsync(true);

        }

        public TopicReadOnlyRepositoryBuilder GetTopicByIdAndUserId(Guid id, Guid userId, Topic topic)
        {
            repository
                .Setup(t => t.GetTopicByIdAndUserId(id, userId))
                .ReturnsAsync(topic);

            return this;
        }

        public void GetTopics(string? description, string? title, YourNotes.Domain.Entities.User? user)
        {
            IList<Topic> topics = new List<Topic>();

            if (!string.IsNullOrEmpty(title) && user is not null && !string.IsNullOrEmpty(description))
            {
                topics.Add(new Topic(user!.Id, title!, description));

            }

            repository
                  .Setup(t => t.GetTopics(user))
                  .ReturnsAsync(topics);
        }

        public TopicReadOnlyRepositoryBuilder TopicExists(Guid userId, Topic? topic = null)
        {

            if (topic is not null)
            {
                repository
                    .Setup(t => t.TopicExists(topic.Id, userId))
                    .ReturnsAsync(true);
            }

            return this;
        }

    }
}
