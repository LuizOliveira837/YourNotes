using CommonTestUtilities.Builders;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;

namespace WebAPI.Test.User
{
    public class UpdateUserNameTest : IClassFixture<CustomWebApplicationFactory>
    {
        public readonly HttpClient _client;
        public readonly CustomWebApplicationFactory _factory;
        public static string METHOD = "user";
        public RequestUpdateUserName? request { get; set; }
        public RequestRegisterUser requestRegisterUser { get; set; }

        public UpdateUserNameTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            requestRegisterUser = RequestRegisterUserBuilder.Build();


        }



        [Theory]
        [InlineData("Teste1")]
        public async Task Sucess(string userName)
        {
            //ARRANGE
            request = new RequestUpdateUserName(userName);
            var jwtGaneratorBuilder = JwtTokenGeneratorBuilder.Build();
            var token = jwtGaneratorBuilder.GenerationToken(_factory.UserId);

            //ACT

            _client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token.ToString()));
            var result = await _client.PatchAsJsonAsync(METHOD, request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);


            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);



            var resultUserName = resultAsJsonDocument.RootElement.GetProperty("username").ToString();


            resultUserName
                .Should()
                .Be(userName);

        }


        [Fact]
        public async Task ERROR_UserName_Already_Exists()
        {
            //ARRANGE
            request = new RequestUpdateUserName(requestRegisterUser.UserName);
            var jwtGaneratorBuilder = JwtTokenGeneratorBuilder.Build();
            var token = jwtGaneratorBuilder.GenerationToken(_factory.UserId);
            await _client.PostAsJsonAsync("user", requestRegisterUser);


            //ACT

            _client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token.ToString()));
            var result = await _client.PatchAsJsonAsync(METHOD, request);


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


        [Theory]
        [InlineData("Teste1")]
        public async Task ERROR_User_Not_Found(string userName)
        {
            //ARRANGE
            request = new RequestUpdateUserName(userName);
            var jwtGaneratorBuilder = JwtTokenGeneratorBuilder.Build();
            var token = jwtGaneratorBuilder.GenerationToken(Guid.NewGuid());

            //ACT

            _client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token.ToString()));
            var result = await _client.PatchAsJsonAsync(METHOD, request);


            //ASSERT

            result
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);


            using var resultAsStream = await result.Content.ReadAsStreamAsync();

            var resultAsJsonDocument = await JsonDocument.ParseAsync(resultAsStream);



            var errors = resultAsJsonDocument.RootElement.GetProperty("errors").EnumerateArray();


            errors
               .Count()
               .Should()
               .Be(1);

            errors
                .Should()
                .ContainSingle(x => x.ToString() == YourNotesExceptionResource.USER_NOT_FOUND);

        }

        [Theory]
        [InlineData("Teste1")]
        public async Task ERROR_USER_UNAUTHORIZED(string userName)
        {
            //ARRANGE
            request = new RequestUpdateUserName(userName);
            var jwtGaneratorBuilder = JwtTokenGeneratorBuilder.Build();
            var token = jwtGaneratorBuilder.GenerationToken(Guid.NewGuid());

            //ACT

            _client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token.ToString()));

            var result = await _client
                .PatchAsJsonAsync(METHOD, request);


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
