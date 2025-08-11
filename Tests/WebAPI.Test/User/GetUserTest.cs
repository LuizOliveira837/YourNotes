using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using YourNotes.Exception;

namespace WebAPI.Test.User
{
    public class GetUserTest(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory = factory;
        private readonly HttpClient _client = factory.CreateClient();
        private static string METHOD = "user";

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(_factory.Id);

            //act
            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            var result = await _client
                .GetAsync(METHOD);


            //assert

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            using var resultAsStream = result.Content.ReadAsStream();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("userName").ToString()
                .Should()
                .Be(_factory.UserName);

            resultAsJson.RootElement.GetProperty("email").ToString()
                .Should()
                .Be(_factory.Email);

            resultAsJson.RootElement.GetProperty("firstName").ToString()
                .Should()
                .Be(_factory.FirstName);

            resultAsJson.RootElement.GetProperty("lastName").ToString()
               .Should()
               .Be(_factory.LastName);

            resultAsJson.RootElement.GetProperty("active").GetBoolean()
               .Should()
               .Be(_factory.Active);



        }

        [Fact]
        public async Task ERROR_USER_UNAUTHORIZED()
        {
            //arrange
            var tokenGeneration = JwtTokenGeneratorBuilder.Build();

            var token = tokenGeneration.GenerationToken(Guid.NewGuid());

            //act
            _client
                .DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            var result = await _client
                .GetAsync(METHOD);


            //assert

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

            using var resultAsStream = result.Content.ReadAsStream();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("errors").EnumerateArray()
                .Any(x => x.ToString() == YourNotesExceptionResource.USER_NOT_FOUND)
                .Should()
                .BeTrue();

        }
    }
}
