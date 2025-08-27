using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.User.UpdateUserName;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.User
{
    public class UpdateUserNameUseCaseTest
    {
        public UnitOfWorkBuilder? uofMoq;
        public UpdateUserNameUseCase UpdateUserNameUseCaseBuild(YourNotes.Domain.Entities.User? user, string userName = "")
        {
            uofMoq = new UnitOfWorkBuilder();
            var readRepository = new UserReadOnlyRepositoryBuilder(user).UserNameExistsAsync(userName).Build();
            var writeRepository = new UserWriteOnlyRepositoryBuilder(user).Build();

            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser;

            return new UpdateUserNameUseCase(uofMoq.uof.Object, readRepository, writeRepository, loggedUser.Object);
        }


        [Theory]
        [InlineData("TESTE1")]

        public async Task Sucess(string userName)
        {
            //arrange
            var user = UserBuilder.Build();
            var useCase = UpdateUserNameUseCaseBuild(user);
            //act

            var result = await useCase.Execute(new RequestUpdateUserName(userName));

            //assert

            result
                .Username
                .Should()
                .Be(userName);

        }

        [Theory]
        [InlineData("TESTE1")]
        public async Task ERRO_UserName_Already_Exists(string userName)
        {
            //arrange
            var user = UserBuilder.Build();


            var useCase = UpdateUserNameUseCaseBuild(user, userName);

            //act

            var result = async () => await useCase.Execute(new RequestUpdateUserName(userName));


            //assert

            await (result
                .Should()
                .ThrowAsync<OnValidationException>())
                .WithMessage(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);

        }


    }
}
