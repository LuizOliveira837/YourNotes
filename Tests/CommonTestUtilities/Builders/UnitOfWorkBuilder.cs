using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories;

namespace CommonTestUtilities.Builders
{
    public class UnitOfWorkBuilder
    {
        public Mock<IUnitOfWork> uof = new();
        public UserRepositoryBuilder userRepositoryMoq = new();
        public UnitOfWorkBuilder(User? user)
        {

            if (user is not null)
            {
                userRepositoryMoq
                    .EmailExistsAsync(user.Email);

                userRepositoryMoq
                     .UserNameExistsAsync(user.UserName);

                userRepositoryMoq
                    .CreateAsync(user.Id);

                userRepositoryMoq
                     .GetAsync(user);
            }
  
 


            uof
                .Setup(x => x.Users).Returns(userRepositoryMoq.userRepository.Object);
        }




    }
}
