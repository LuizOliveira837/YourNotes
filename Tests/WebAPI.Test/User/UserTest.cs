using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;

namespace WebAPI.Test.User
{
    public class UserTest : IClassFixture<CustomWebApplicationFactory>
    {
        public readonly HttpClient _client;
        public readonly CustomWebApplicationFactory _factory;
        public RequestRegisterUser request { get; set; }
        public UserTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            request = RequestRegisterUserBuilder.Build();

        }



        [Fact]
        public async Task Sucess()
        {
            //ARRANGE

            //ACT

            var result = await _client.PostAsJsonAsync("user", request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);



            Guid.TryParse(resultAsJsonDocument.RootElement.GetProperty("id").ToString(), out var id)
                .Should()
                .BeTrue();

            id.Should().NotBe(Guid.Empty);


            var token = resultAsJsonDocument.RootElement.GetProperty("token").GetProperty("accessToken");

            token.GetString().Should().NotBeNullOrWhiteSpace();

        }


        [Fact]
        public async Task ERROR_INVALID_EMAIL()
        {
            //ARRANGE
            request.Email = "luizhorochagmail.com";

            //ACT

            var result = await _client.PostAsJsonAsync("user", request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);


            var errors = resultAsJsonDocument.RootElement.GetProperty("errors").EnumerateArray();

            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Should()
                .ContainSingle(x => x.ToString() == YourNotesExceptionResource.INVALID_EMAIL);

        }


        [Fact]
        public async Task ERROR_EMAIL_ALREADY_EXISTS()
        {
            //ARRANGE
            request.Email = _factory.Email;

            //ACT

            var result = await _client.PostAsJsonAsync("user", request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);


            var errors = resultAsJsonDocument.RootElement.GetProperty("errors").EnumerateArray();

            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Should()
                .ContainSingle(x => x.ToString() == YourNotesExceptionResource.EMAIL_ALREADY_EXISTS);

        }

        [Fact]
        public async Task ERROR_USERNAME_ALREADY_EXISTS()
        {
            //ARRANGE
            request.UserName = _factory.UserName;

            //ACT

            var result = await _client.PostAsJsonAsync("user", request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);


            var errors = resultAsJsonDocument.RootElement.GetProperty("errors").EnumerateArray();

            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Should()
                .ContainSingle(x => x.ToString() == YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);

        }

        [Fact]
        public async Task ERROR_PASSWORD_INVALID()
        {
            //ARRANGE
            request.Password = string.Empty;

            //ACT

            var result = await _client.PostAsJsonAsync("user", request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);


            var errors = resultAsJsonDocument.RootElement.GetProperty("errors").EnumerateArray();

            errors
                .Count()
                .Should()
                .Be(1);

            errors
                .Should()
                .ContainSingle(x => x.ToString() == YourNotesExceptionResource.INVALID_PASSWORD);

        }

        

    }
}
