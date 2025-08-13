using FluentAssertions;
using YourNotes.Application.User.UpdateUserName;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;

namespace Validators.Test.User
{
    public class UpdateUserNameValidateTest
    {


        [Fact]
        public void Sucess()
        {
            //arrange 
            var validator = new UpdateUserNameValidate();
            var request = new RequestUpdateUserName("LROCHA123@");

            //act
            var result = validator.Validate(request);

            //assert

            result
                .IsValid
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ERROR_UserName_Empty()
        {
            //arrange 
            var validator = new UpdateUserNameValidate();
            var request = new RequestUpdateUserName("");

            //act
            var result = validator.Validate(request);

            //assert

            result
                .IsValid
                .Should()
                .BeFalse();

            result
                .Errors
                .Count()
                .Should()
                .Be(1);

            result
                .Errors
                .Any(e=> e.ErrorMessage == YourNotesExceptionResource.USERNAME_INVALID)
                .Should()
                .BeTrue();
        }
        
    }
}
