using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Login.LoginByEmailAndPassword;
using YourNotes.Exception;

namespace Validators.Test.Login
{
    public class LoginByEmailAndPasswordValidateTest
    {

        [Fact]
        public void Sucess()
        {
            //arrange

            var validator = new LoginByEmailAndPasswordValidate();
            var request = RequestLoginByEmailAndPasswordBuilder.Builder();

            //act

            var result = validator
                .Validate(request);


            //assert

            result
                .IsValid
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("T")]
        [InlineData("Teste")]
        [InlineData("Teste123")]
        [InlineData("teste123@")]
        public void ERROR_Password_invalid(string password)
        {
            //arrange

            var validator = new LoginByEmailAndPasswordValidate();
            var request = RequestLoginByEmailAndPasswordBuilder.Builder();

            request.Password = password;
            //act

            var result = validator
                .Validate(request);


            //assert

            result
                .IsValid
                .Should()
                .BeFalse();

            result
                .Errors.Count()
                .Should()
                .Be(1);

            result
               .Errors.Any(e=> e.ErrorMessage == YourNotesExceptionResource.INVALID_PASSWORD)
               .Should()
               .BeTrue();
        }

        [Theory]
        [InlineData("teste")]
        [InlineData("testegmail.com")]
        [InlineData("teste@com")]
        public void ERROR_Email_invalid(string email)
        {
            //arrange

            var validator = new LoginByEmailAndPasswordValidate();
            var request = RequestLoginByEmailAndPasswordBuilder.Builder();

            request.Email = email;
            request.Password = "Teste123@!";
            //act

            var result = validator
                .Validate(request);


            //assert

            result
                .IsValid
                .Should()
                .BeFalse();

            result
                .Errors.Count()
                .Should()
                .Be(1);

            result
               .Errors.Any(e => e.ErrorMessage == YourNotesExceptionResource.INVALID_EMAIL)
               .Should()
               .BeTrue();
        }
    }
}
