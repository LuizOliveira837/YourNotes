using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebAPI.Test.Topic
{
    public class GetTopicTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private const string METHOD = "topic";

        public GetTopicTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange

            var token = JwtTokenGeneratorBuilder.Build().GenerationToken(_factory.UserId);


            //act
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            var result = await _client.GetAsync(METHOD);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();
            var resultAsJson = JsonDocument.Parse(resultAsStream);

            var topics = resultAsJson.RootElement.GetProperty("topics").EnumerateArray()
                .Select(t => t.GetProperty("title").ToString()).ToList();

            //assert

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            topics
                .Count()
                .Should()
                .Be(1);

            topics
                .Any(t => t == _factory.Title)
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task Sucess_Empty_List()
        {
            //arrange

            var request = RequestRegisterUserBuilder.Build();



            //act

            
            var resultCreateUser = await _client.PostAsJsonAsync("user", request);
            using var resultCreateUserAsStream = await resultCreateUser.Content.ReadAsStreamAsync();
            using var resultCreateUserAsJson = JsonDocument.Parse(resultCreateUserAsStream);
            var token = resultCreateUserAsJson.RootElement.GetProperty("token").GetProperty("accessToken");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);



            var result = await _client.GetAsync(METHOD);

          
            //assert
            resultCreateUser
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);


            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.NoContent);

        }
    }
}
