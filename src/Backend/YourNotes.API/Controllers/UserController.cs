using Microsoft.AspNetCore.Mvc;
using YourNotes.Communication.Request;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUser request)
        {
           var response =  await useCase.Execute(request);
            return Ok(response);
        }
    }
}
