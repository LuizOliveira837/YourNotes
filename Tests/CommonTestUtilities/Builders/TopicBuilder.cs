using Bogus;

namespace CommonTestUtilities.Builders
{
    public static class TopicBuilder
    {
        public static YourNotes.Domain.Entities.Topic Build(Guid userId)
        {
            return new Faker<YourNotes.Domain.Entities.Topic>()
                .RuleFor(t => t.UserId, t => userId)
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence());
        }
    }
}
