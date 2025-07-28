using Microsoft.AspNetCore.Mvc;
using YourNotes.API.Attributes;
using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUser), StatusCodes.Status200OK)]

        public async Task<IActionResult> RegisterUser([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUser request)
        {
            var response =  await useCase.Execute(request);
            
            return Ok(response);
        }

        [HttpPatch]
        [AuthenticatedUser]
        public async Task<IActionResult> UpdateUserName([FromServices] IUpdateUserNameUseCase useCase, [FromBody] RequestUpdateUserName request, [FromHeader] string userIdentifier)
        {
            var result = await useCase.Execute(new Guid(userIdentifier), request);

            return Ok(result);
        }

        [HttpGet]
        [AuthenticatedUser] 
        [ProducesResponseType(typeof(ResponseGetUser), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetUserById([FromServices] IGetUserByIdUseCase useCase, [FromHeader] string userIdentifier)
        {
            var result = await useCase.Execute(new Guid(userIdentifier));

            return Ok(result);
        }
    }
}
