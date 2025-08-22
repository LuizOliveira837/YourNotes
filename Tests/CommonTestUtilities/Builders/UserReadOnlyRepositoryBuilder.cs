using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.User;

namespace CommonTestUtilities.Builders
{
    public class UserReadOnlyRepositoryBuilder
    {
        public Mock<IUserReadOnlyRepository> repository;
        public UserReadOnlyRepositoryBuilder(User? user)
        {
            repository = new Mock<IUserReadOnlyRepository>();

            if (user is not null)
            {
                EmailExistsAsync(user.Email);

                UserNameExistsAsync(user.UserName);

                GetAsync(user);
            }

        }

        public IUserReadOnlyRepository Build() => repository.Object;

        public void EmailExistsAsync(string email)
        {
            repository
               .Setup(x => x.EmailExistsAsync(email))
               .Returns(Task.FromResult(true));

        }
        public UserReadOnlyRepositoryBuilder UserNameExistsAsync(string userName)
        {
            repository
               .Setup(x => x.UserNameExistsAsync(userName))
               .Returns(Task.FromResult(true));

            return this;

        }

        public void GetAsync(User user)
        {
            repository
               .Setup(x => x.GetAsync(It.IsAny<Guid>()))
               .Returns(Task.FromResult(user)!);


        }
    }
}
