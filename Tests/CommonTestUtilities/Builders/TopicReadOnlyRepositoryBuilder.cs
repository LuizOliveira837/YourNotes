using Moq;
using YourNotes.Domain.Interfaces.Repositories.Topic;

namespace CommonTestUtilities.Builders
{
    public class TopicReadOnlyRepositoryBuilder
    {
        public Mock<ITopicReadOnlyRepository> repository;

        public TopicReadOnlyRepositoryBuilder(string? title, Guid id)
        {
            repository = new();

            if (!string.IsNullOrEmpty(title))
            {
                TopicAlreadyExists(title, id);
            }
        }

        public void TopicAlreadyExists(string title, Guid id)
        {
            repository
                .Setup(t => t.TopicAlreadyExists(title, id))
                .ReturnsAsync(true);

        }

    }
}
