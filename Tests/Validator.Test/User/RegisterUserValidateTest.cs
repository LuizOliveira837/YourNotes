using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Exception;

namespace Validators.Test.User
{
    public class RegisterUserValidateTest
    {


        [Fact]
        public void Sucess()
        {
            //arrange

            var validator = RegisterUserValidateBuilder.Build();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();

            //acct

            var result = validator.Validate(requestRegisterUser);


            //assert
            result
                .IsValid
                .Should().BeTrue();

        }

        [Theory]
        [InlineData("teste")]
        [InlineData("teste123")]
        [InlineData("Teste123")]
        [InlineData("Teste123@")]
        public void ERROR_PASSWORD_INVALID(string password)
        {
            //arrange

            var validator = RegisterUserValidateBuilder.Build();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            requestRegisterUser.Password = password;

            //acct

            var result = validator.Validate(requestRegisterUser);


            //assert

            result
             .Errors
             .Count()
             .Should()
             .Be(1);

            result
             .Errors
             .Should()
             .ContainSingle(x => x.ErrorMessage == YourNotesExceptionResource.INVALID_PASSWORD);

        }

        [Theory]
        [InlineData("luizhorochagmail.com")]
        [InlineData("luizhorocha@gmailcom")]
        [InlineData("luizhorocha@")]
        public void ERROR_EMAIL_INVALID(string email)
        {
            //arrange

            var validator = RegisterUserValidateBuilder.Build();
            var requestRegisterUser = RequestRegisterUserBuilder.Build();
            requestRegisterUser.Email = email;

            //act

            var result = validator.Validate(requestRegisterUser);


            //assert
   
            result
             .Errors
             .Count
             .Should()
             .Be(1);

            result
             .Errors
             .Should()
             .ContainSingle(x => x.ErrorMessage == YourNotesExceptionResource.INVALID_EMAIL);

        }
    }
}
