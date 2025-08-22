using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Topic;
using YourNotes.Communication.Requests.Topic;

namespace Validators.Test.Topic
{
    public class ValidateTopicTest
    {


        [Fact]
        public void Sucess()
        {
            //arrange

            var validator = new ValidateTopic();

            var request = RequestTopicJsonBuilder.Build();

            //act

            var result = validator.Validate(request);

            //assert

            result
                .IsValid
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void ERROR_TITLE_INVALID(string title)
        {
            //arrange

            var validator = new ValidateTopic();

            var request = new RequestTopicJson()
            {
                Title = title
            };

            //act

            var result = validator.Validate(request);

            //assert

            result
                .IsValid
                .Should()
                .BeFalse();
        }


    }
}
