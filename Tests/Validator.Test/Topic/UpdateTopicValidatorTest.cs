using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Topic.UpdateTopic;
using YourNotes.Exception;

namespace Validators.Test.Topic
{
    public class UpdateTopicValidatorTest
    {



        [Fact]
        public void Sucess()
        {
            //arrange

            var validator = new UpdateTopicValidator();
            var request = RequestUpdateTopicJsonBuilder.Build();
            //act

            var result = validator.Validate(request);


            //assert

            result
                .IsValid
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("   ")]
        [InlineData("")]
        public void ERROR_EMPTY_TITLE(string newTitle)
        {
            //arrange

            var validator = new UpdateTopicValidator();
            var request = RequestUpdateTopicJsonBuilder.Build();
            request.NewTitle = newTitle;
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
                .Select(x=> x.ErrorMessage)
                .First()
                .Should()
                .Be(YourNotesExceptionResource.INVALID_TITLE);
        }
    }
}
