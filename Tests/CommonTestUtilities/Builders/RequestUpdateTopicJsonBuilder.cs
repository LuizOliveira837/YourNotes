using Bogus;
using YourNotes.Communication.Requests.Topic;

namespace CommonTestUtilities.Builders
{
    public static class RequestUpdateTopicJsonBuilder
    {
        public static RequestUpdateTopicJson Build(Guid? titleId = null)
        {
            return new Faker<RequestUpdateTopicJson>()
                .RuleFor(t => t.Id, f => titleId is null? Guid.NewGuid() : titleId)
                .RuleFor(t => t.NewTitle, f => f.Lorem.Word());

        }
    }
}
