using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.User.GetUserById;

namespace UseCases.Test.User
{
    public class GetUserByIdUseCaseTest
    {

        public static GetUserByIdUseCase GetUserByIdUseCaseBuild(YourNotes.Domain.Entities.User? user)
        {

            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var mapper = MapperBuilder.Build();
            return new GetUserByIdUseCase(loggedUser, mapper);
        }



        [Fact]
        public async Task Sucess()
        {
            //arrange
            var user = UserBuilder.Build();
            var useCase = GetUserByIdUseCaseBuild(user);


            //act
            var result = await useCase.Execute();


            //Assert

            result
                .UserName
                .Should()
                .Be(user.UserName);

            result
               .FirstName
               .Should()
               .Be(user.FirstName);

            result
               .LastName
               .Should()
               .Be(user.LastName);

            result
               .Email
               .Should()
               .Be(user.Email);

            result
               .Active
               .Should()
               .Be(user.Active);
        }
    }
}
