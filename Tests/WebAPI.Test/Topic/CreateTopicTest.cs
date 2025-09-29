
using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Exception;

namespace WebAPI.Test.Topic
{
    public class CreateTopicTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        public const string METHOD = "topic";

        public CreateTopicTest(CustomWebApplicationFactory factory)

        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            //arrange

            var request = RequestTopicJsonBuilder.Build();
            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.UserId);

            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            //act
            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            //assert

            var title = resultAsJson.RootElement.GetProperty("title").ToString();
            var id = resultAsJson.RootElement.GetProperty("id").ToString();

            var resultParse = Guid.TryParse(id, out Guid guid);

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            title
                .Should()
                .Be(request.Title);

            resultParse
                .Should()
                .BeTrue();

            guid.Should().NotBe(Guid.Empty);


        }


        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ERROR_INVALID_TITLE(string title)
        {
            //arrange

            var request = RequestTopicJsonBuilder.Build();
            request.Title = title;

            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.UserId);

            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            //act
            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            //assert

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            var errors = resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Select(x => x.ToString());


            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Any(e => e == YourNotesExceptionResource.INVALID_TITLE)
                .Should()
                .BeTrue();




        }

        [Fact]
        public async Task ERROR_TITLE_ALREADY_EXISTS()
        {
            //arrange

            var request = RequestTopicJsonBuilder.Build();
            request.Title = _factory.Title;

            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.UserId);

            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            //act
            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            //assert

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            var errors = resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Select(x => x.ToString());


            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Any(e => e == YourNotesExceptionResource.TITLE_ALREADY_EXISTS)
                .Should()
                .BeTrue();

        }
    }
}
