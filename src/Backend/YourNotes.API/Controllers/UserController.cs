using Microsoft.AspNetCore.Mvc;
using YourNotes.Communication.Requests.User;
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
        public async Task<IActionResult> UpdateUserName([FromServices] IUpdateUserNameUseCase useCase, [FromBody] RequestUpdateUserName request)
        {
            var id = new Guid("CF9AEE29-1264-4C84-9ED1-471162A3212C");
            var result = await useCase.Execute(id, request);

            return Ok(result);
        }
    }
}
