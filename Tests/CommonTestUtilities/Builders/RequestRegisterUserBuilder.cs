using Bogus;
using Bogus.DataSets;
using YourNotes.Communication.Requests.User;

namespace CommonTestUtilities.Builders
{
    public static class RequestRegisterUserBuilder
    {


        public static RequestRegisterUser Build()
        {
            var faker = new Faker<RequestRegisterUser>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, (f,u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Password, f => f.Internet.GeneratePassword());

            return faker.Generate();
        }


        public static string GeneratePassword(this Internet internet)
        {
            var password = internet.Password(20);


            var passwordSplit = password.ToCharArray();

            passwordSplit[0] = Char.Parse(passwordSplit[0].ToString().ToUpper());
            passwordSplit[1] = Char.Parse(passwordSplit[1].ToString().ToLower());
            passwordSplit[3] = '1';
            passwordSplit[4] = '@';

            return  string.Concat(passwordSplit);
             
        }
    }
}
