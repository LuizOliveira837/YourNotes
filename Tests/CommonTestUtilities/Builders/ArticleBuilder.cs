using Bogus;
using YourNotes.Domain.Entities;

namespace CommonTestUtilities.Builders
{
    public static class ArticleBuilder
    {

        public static Article Build(Guid? topicId=null)
        {

            var faker = new Faker<Article>()
                .RuleFor(a => a.Title, f => f.Lorem.Letter(7))
                .RuleFor(a => a.Description, f => f.Lorem.Sentences(1))
                .RuleFor(a => a.TopicId, () => topicId ?? Guid.NewGuid());

            return faker;
        }
    }
}
