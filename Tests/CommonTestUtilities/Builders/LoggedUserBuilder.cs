using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Services;

namespace CommonTestUtilities.Builders
{
    public class LoggedUserBuilder
    {
        public Mock<ILoggedUser> loggedUser = new();

        public LoggedUserBuilder Build(User? user)
        {
            if (user is not null)
            {

                loggedUser
                    .Setup(x => x.User())
                    .ReturnsAsync(user);
            }


            return this;
        }
    }
}
