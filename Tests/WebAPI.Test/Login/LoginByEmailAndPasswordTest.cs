using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Communication.Requests.Login;
using YourNotes.Exception;

namespace WebAPI.Test.Login
{
    public class LoginByEmailAndPasswordTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private static string METHOD = "login";

        public LoginByEmailAndPasswordTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            //ARRANGE
            var request = new RequestLoginByEmailAndPassword()
            {
                Email = _factory.Email,
                Password = _factory.Password
            };

            //ACT

            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            var id = resultAsJson.RootElement.GetProperty("id").GetGuid();
            var token = resultAsJson.RootElement.GetProperty("token").GetProperty("accessToken").ToString();

            id.Should().Be(_factory.UserId);

            token.Should().BeOfType<string>();

            string.IsNullOrEmpty(token).Should().BeFalse();

        }


        [Fact]
        public async Task ERROR_Password_INVALID()
        {
            //ARRANGE
            var request = new RequestLoginByEmailAndPassword()
            {
                Email = _factory.Email,
                Password = string.Empty
            };

            //ACT

            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Count()
                .Should()
                .Be(1);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Any(e => e.ToString() == YourNotesExceptionResource.INVALID_PASSWORD)
                .Should()
                .BeTrue();


        }

        [Fact]
        public async Task ERROR_Email_INVALID()
        {
            //ARRANGE
            var request = new RequestLoginByEmailAndPassword()
            {
                Email = "testegmail.com",
                Password = _factory.Password,
            };

            //ACT

            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Count()
                .Should()
                .Be(1);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Any(e => e.ToString() == YourNotesExceptionResource.INVALID_EMAIL)
                .Should()
                .BeTrue();


        }


        [Fact]
        public async Task ERROR_Password_Unauthorized()
        {
            //ARRANGE
            var request = new RequestLoginByEmailAndPassword()
            {
                Email = _factory.Email,
                Password = "Teste123@teste!"
            };

            //ACT

            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Count()
                .Should()
                .Be(1);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Any(e => e.ToString() == YourNotesExceptionResource.USER_NOT_FOUND)
                .Should()
                .BeTrue();


        }

        [Fact]
        public async Task ERROR_Email_Unauthorized()
        {
            //ARRANGE
            var request = new RequestLoginByEmailAndPassword()
            {
                Email = "teste@gmail.com",
                Password = _factory.Password,
            };

            //ACT

            var result = await _client
                .PostAsJsonAsync(METHOD, request);

            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJson = JsonDocument.Parse(resultAsStream);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Count()
                .Should()
                .Be(1);

            resultAsJson.RootElement.GetProperty("errors")
                .EnumerateArray()
                .Any(e => e.ToString() == YourNotesExceptionResource.USER_NOT_FOUND)
                .Should()
                .BeTrue();


        }


    }
}
