using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Exception;

namespace WebAPI.Test.Topic
{
    public class UpdateTopic : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string METHOD = "topic";

        public UpdateTopic(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var request = RequestUpdateTopicJsonBuilder.Build(_factory.TopicId);
            var generator = JwtTokenGeneratorBuilder.Build();
            var token = generator.GenerationToken(_factory.UserId);

            //act
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var result = await _client.PutAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();
            var resultAsJson = JsonDocument.Parse(resultAsStream);

            var topic = resultAsJson.RootElement.GetProperty("title").ToString();
            var id = resultAsJson.RootElement.GetProperty("id").ToString();

            //assert

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            topic
                .Should()
                .Be(request.NewTitle);

            id.Should().Be(id);


        }
        [Fact]
        public async Task ERROR_TITLE_NOT_FOUND()
        {
            //arrange
            var request = RequestUpdateTopicJsonBuilder.Build();
            var generator = JwtTokenGeneratorBuilder.Build();
            var token = generator.GenerationToken(_factory.UserId);

            //act
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var result = await _client.PutAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();
            var resultAsJson = JsonDocument.Parse(resultAsStream);

            var errors = resultAsJson.RootElement.GetProperty("errors").EnumerateArray().Select(x => x.ToString()).ToList();

            //assert

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Any(x => x == YourNotesExceptionResource.TOPIC_NOT_FOUND)
                .Should()
                .BeTrue();


        }
    }
}
