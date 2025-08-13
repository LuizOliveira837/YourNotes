using Bogus;
using CommonTestUtilities.Services;
using YourNotes.Communication.Requests.Login;

namespace CommonTestUtilities.Builders
{
    public static class RequestLoginByEmailAndPasswordBuilder
    {

        public static RequestLoginByEmailAndPassword Builder(int length = 10)
        {
            var builder = new Faker<RequestLoginByEmailAndPassword>()
                .RuleFor(l => l.Email, f => f.Internet.Email())
                .RuleFor(l => l.Password, f => f.Internet.GeneratePassword());


            return builder;
        }
    }
}
