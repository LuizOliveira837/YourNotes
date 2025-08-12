using Microsoft.AspNetCore.Mvc;
using YourNotes.Communication.Requests.Login;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.API.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public LoginController()
        {

        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginByEmailAndPassword([FromBody] RequestLoginByEmailAndPassword request,
            [FromServices] ILoginByEmailAndPasswordUseCase useCase)
        {
           var result =  await useCase.Execute(request);


            return Ok(result);
        }
    }
}
