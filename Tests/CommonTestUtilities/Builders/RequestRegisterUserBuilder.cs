using Bogus;
using YourNotes.Communication.Requests.User;
using CommonTestUtilities.Services;

namespace CommonTestUtilities.Builders
{
    public static class RequestRegisterUserBuilder
    {


        public static RequestRegisterUser Build()
        {
            var faker = new Faker<RequestRegisterUser>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Password, f => f.Internet.GeneratePassword());


            return faker.Generate();
        }

    }
}
