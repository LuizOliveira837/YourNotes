using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories;

namespace CommonTestUtilities.Builders
{
    public class UserRepositoryBuilder
    {
        public Mock<IUserRepository> userRepository = new();



        public void EmailExistsAsync(string email)
        {
            userRepository
               .Setup(x => x.EmailExistsAsync(email))
               .Returns(Task.FromResult(true));

        }
        public void UserNameExistsAsync(string userName)
        {
            userRepository
               .Setup(x => x.UserNameExistsAsync(userName))
               .Returns(Task.FromResult(true));

        }

        public void CreateAsync(Guid id)
        {
            userRepository
               .Setup(x => x.CreateAsync(It.IsAny<User>()))
               .Returns(Task.FromResult(id));


        }


    }
}
