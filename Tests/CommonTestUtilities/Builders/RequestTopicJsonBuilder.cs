using Bogus;
using YourNotes.Communication.Requests.Topic;

namespace CommonTestUtilities.Builders
{
    public static class RequestTopicJsonBuilder
    {

        public static RequestTopicJson Build()
        {
            return new Faker<RequestTopicJson>()
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.Description, f => f.Lorem.Sentence());
        }
    }
}
