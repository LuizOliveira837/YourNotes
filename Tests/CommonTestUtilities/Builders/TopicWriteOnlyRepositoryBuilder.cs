using Moq;
using YourNotes.Domain.Interfaces.Repositories.Topic;

namespace CommonTestUtilities.Builders
{
    public class TopicWriteOnlyRepositoryBuilder
    {
        public Mock<ITopicWriteOnlyRepository> repository;
        public TopicWriteOnlyRepositoryBuilder(YourNotes.Domain.Entities.Topic? topic)
        {
            repository = new();

            if (topic is not null)
            {
                CreateAsync(topic);
            }

        }

        public void CreateAsync(YourNotes.Domain.Entities.Topic topic)
        {
            repository
                .Setup(t => t.CreateAsync(It.IsAny<YourNotes.Domain.Entities.Topic>()))
                .ReturnsAsync(topic.Id);
        }

       
    }
}
