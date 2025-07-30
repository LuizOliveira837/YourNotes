using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.User.UpdateUserName;
using YourNotes.Communication.Requests.User;
using YourNotes.Domain.Entities;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.User
{
    public class UpdateUserNameUseCaseTest
    {
        public UnitOfWorkBuilder? uofMoq;
        public UpdateUserNameUseCase UpdateUserNameUseCaseBuild(YourNotes.Domain.Entities.User user)
        {
            uofMoq = new UnitOfWorkBuilder(user);
            return new UpdateUserNameUseCase(uofMoq.uof.Object);

        }


        [Theory]
        [InlineData("TESTE1")]

        public async Task Sucess(string userName)
        {
            //arrange
            var user = UserBuilder.Build();
            var useCase = UpdateUserNameUseCaseBuild(user);
            //act

            var result = await useCase.Execute(user.Id, new RequestUpdateUserName(userName));


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


            var useCase = UpdateUserNameUseCaseBuild(user);

            uofMoq!.userRepositoryMoq.UserNameExistsAsync(userName);
            //act

            var result = async () => await useCase.Execute(user.Id, new RequestUpdateUserName(userName));


            //assert

            await (result
                .Should()
                .ThrowAsync<OnValidationException>())
                .WithMessage(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);



        }

        [Theory]
        [InlineData("TESTE1")]
        public async Task ERRO_User_Not_Exists(string userName)
        {
            //arrange
            var user = UserBuilder.Build();
            user.Id = Guid.Empty;

            var useCase = UpdateUserNameUseCaseBuild(user);

            //act

            var result = async () => await useCase.Execute(user.Id, new RequestUpdateUserName(userName));


            //assert

            await (result
                .Should()
                .ThrowAsync<OnValidationException>())
                .WithMessage(YourNotesExceptionResource.USER_NOT_FOUND);



        }

    }
}
