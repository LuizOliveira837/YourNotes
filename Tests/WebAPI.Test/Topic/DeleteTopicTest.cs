
using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using YourNotes.Exception;

namespace WebAPI.Test.Topic
{
    public class DeleteTopicTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private string METHOD = "topic";

        public DeleteTopicTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var token = JwtTokenGeneratorBuilder.Build().GenerationToken(_factory.UserId);
            METHOD = $"{METHOD}/{_factory.TopicId}";

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);

            //act
            var result = await _client.DeleteAsync(METHOD);

            //assert
            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task Error_Topic_Not_Found()
        {
            //arrange
            var token = JwtTokenGeneratorBuilder.Build().GenerationToken(_factory.UserId);
            METHOD = $"{METHOD}/{Guid.NewGuid()}";

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //act
            var result = await _client.DeleteAsync(METHOD);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();
            var resultAsJson = JsonDocument.Parse(resultAsStream);

            var errors = resultAsJson.RootElement.GetProperty("errors").EnumerateArray();

            //assert
            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);

            errors
                .Any(e => e.ToString() == YourNotesExceptionResource.TOPIC_NOT_EXISTS)
                .Should()
                .BeTrue();

            errors
                .Count()
                .Should()
                .Be(1);

        }
    }
}
