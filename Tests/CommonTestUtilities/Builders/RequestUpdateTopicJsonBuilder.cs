using Bogus;
using YourNotes.Communication.Requests.Topic;

namespace CommonTestUtilities.Builders
{
    public static class RequestUpdateTopicJsonBuilder
    {
        public static RequestUpdateTopicJson Build(Guid? userId = null)
        {
            return new Faker<RequestUpdateTopicJson>()
                .RuleFor(t => t.Id, f => userId is null? Guid.NewGuid() : userId)
                .RuleFor(t => t.NewTitle, f => f.Lorem.Word());

        }
    }
}
