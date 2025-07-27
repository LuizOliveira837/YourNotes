using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.User.RegisterUser;
using YourNotes.Communication.Responses;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.User
{
    public class RegisterUserUseCaseTest
    {


        public RegisterUserUseCase RegisterUserUseCaseBuild(Guid? id, string email="", string userName = "")
        {
            var uof = new UnitOfWorkBuilder(id, email, userName);
            var mapper = MapperBuilder.Builder();
            var passwordEncrypter = PasswordEncrypterBuilder.Build();
            var jwtToken = JwtTokenGeneratorBuilder.Build();

            return new RegisterUserUseCase(mapper, uof.uof.Object, passwordEncrypter, jwtToken);

        }


        [Fact]

        public async Task Sucess()
        {
            //arrange
            var id = Guid.NewGuid();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var userCase = RegisterUserUseCaseBuild(id);

            //act

            var result = await userCase.Execute(requestRegisterUser);


            //assert
           
            result
                .Id
                .Should()
                .Be(id);


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
            var id = Guid.NewGuid();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var userCase = RegisterUserUseCaseBuild(null, requestRegisterUser.Email);

            //act

             var result = async () =>  await userCase.Execute(requestRegisterUser);



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
            var id = Guid.NewGuid();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            var userCase = RegisterUserUseCaseBuild(null, string.Empty, requestRegisterUser.UserName);

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
