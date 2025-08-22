using Moq;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.User;

namespace CommonTestUtilities.Builders
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public Mock<IUserWriteOnlyRepository> repository;

        public UserWriteOnlyRepositoryBuilder(User? user)
        {
            repository = new Mock<IUserWriteOnlyRepository>();

            if (user is not null)
            {
                CreateAsync(user.Id);
            }
        }

        public IUserWriteOnlyRepository Build() =>  repository.Object;
 

        public void CreateAsync(Guid id)
        {
            repository
               .Setup(x => x.CreateAsync(It.IsAny<User>()))
               .Returns(Task.FromResult(id));
        }
    }
}
