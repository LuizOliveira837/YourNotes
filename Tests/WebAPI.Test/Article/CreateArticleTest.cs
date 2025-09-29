using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Communication.Requests.Article;
using YourNotes.Exception;

namespace WebAPI.Test.Article
{
    public class CreateArticleTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string METHOD = "article";

        public CreateArticleTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var article = ArticleBuilder.Build(_factory.TopicId);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = article.Title,
                TopicId = article.TopicId,
            };
            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.UserId);

            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //act
            _client.DefaultRequestHeaders.Add("Autorization", "Bearer " + token);
            var result = await _client.PostAsJsonAsync(METHOD, request);

            //assert
            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task ERROR_INVALID_TITLE()
        {
            //arrange
            var article = ArticleBuilder.Build(_factory.TopicId);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = string.Empty,
                TopicId = article.TopicId,
            };
            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.UserId);

            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //act
            _client.DefaultRequestHeaders.Add("Autorization", "Bearer " + token);
            var result = await _client.PostAsJsonAsync(METHOD, request);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();
            var resultAsJson = JsonDocument.Parse(resultAsStream);

            //assert
            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            resultAsJson
                .RootElement
                .GetProperty("errors")
                .EnumerateArray()
                .Select(e => e.ToString())
                .Should()
                .ContainSingle(e => e == YourNotesExceptionResource.INVALID_ARTICLE_LENGTH);
        }
    }
}
