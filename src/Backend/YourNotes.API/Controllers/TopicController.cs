using Microsoft.AspNetCore.Mvc;
using YourNotes.API.Attributes;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.API.Controllers
{
    [Route("[controller]")]
    [AuthenticatedUser]
    [ApiController]
    public class TopicController : Controller
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTopic([FromBody] RequestTopicJson request, [FromServices] ICreateTopicUseCase useCase)
        {
           var id = await useCase.Execute(request);

            return Created("", id);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromServices] IGetTopicUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

    }
}
