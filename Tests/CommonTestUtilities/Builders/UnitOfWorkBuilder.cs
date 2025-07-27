using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourNotes.Domain.Interfaces.Repositories;

namespace CommonTestUtilities.Builders
{
    public class UnitOfWorkBuilder
    {
        public Mock<IUnitOfWork> uof = new();
        public UnitOfWorkBuilder(Guid? id , string email = "", string userName="")
        {
            var userRepositoryMoq = new UserRepositoryBuilder();

            if (!string.IsNullOrEmpty(email))
            {
                userRepositoryMoq
                    .EmailExistsAsync(email);
            }

            if (id is not null)
            {
                userRepositoryMoq
                     .CreateAsync((Guid)id);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                userRepositoryMoq
                     .UserNameExistsAsync(userName);
            }


            uof
                .Setup(x => x.Users).Returns(userRepositoryMoq.userRepository.Object);
        }




    }
}
