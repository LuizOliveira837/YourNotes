using Azure.Core;
using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.User.RegisterUser;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.User
{
    public class RegisterUserUseCaseTest
    {


        public RegisterUserUseCase RegisterUserUseCaseBuild(YourNotes.Domain.Entities.User? user)
        {
            var uof = new UnitOfWorkBuilder(user);
            var mapper = MapperBuilder.Builder();
            var passwordEncrypter = PasswordEncrypterBuilder.Build();
            var jwtToken = JwtTokenGeneratorBuilder.Build();

            return new RegisterUserUseCase(mapper, uof.uof.Object, passwordEncrypter, jwtToken);

        }


        [Fact]

        public async Task Sucess()
        {
            //arrange
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var id = Guid.NewGuid();

            var user = new YourNotes.Domain.Entities.User()
            {
                Id = id
            };



            var userCase = RegisterUserUseCaseBuild(user);

            //act

            var result = await userCase.Execute(requestRegisterUser);


            //assert

            result
                .Id
                .Should()
                .Be(user.Id);


            result
                .Token
                .Should()
                .NotBeNull();

            result
                .Token.AccessToken
                .Should()
                .NotBeEmpty();

        }

        [Fact]
        public async Task ERRO_Email_Already_Exists()
        {
            //arrange
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var user = new YourNotes.Domain.Entities.User()
            {
                Email = requestRegisterUser.Email
            };
            var userCase = RegisterUserUseCaseBuild(user);

            //act

            var result = async () => await userCase.Execute(requestRegisterUser);



            //assert

            await result
                .Should()
                .ThrowAsync<OnValidationException>();

            await result
              .Should()
              .ThrowAsync<OnValidationException>()
              .WithMessage(YourNotesExceptionResource.EMAIL_ALREADY_EXISTS);




        }

        [Fact]
        public async Task ERRO_UserName_Already_Exists()
        {
            //arrange
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var user = new YourNotes.Domain.Entities.User()
            {
                UserName = requestRegisterUser.UserName
            };
            var userCase = RegisterUserUseCaseBuild(user);
            //act

            var result = async () => await userCase.Execute(requestRegisterUser);


            //assert

            await result
                .Should()
                .ThrowAsync<OnValidationException>();

            await result
              .Should()
              .ThrowAsync<OnValidationException>()
              .WithMessage(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);

        }
    }
}
