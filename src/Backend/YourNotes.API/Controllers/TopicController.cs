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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTopic([FromBody] RequestTopicJson request, [FromServices] ICreateTopicUseCase useCase)
        {
           var id = await useCase.Execute(request);

            return Created("", id);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromServices] IGetTopicUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Topics.Any()) return Ok(response);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult>
            Put([FromBody] RequestUpdateTopicJson request, [FromServices] IUpdateTopicUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult>
            Delete([FromBody] RequestUpdateTopicJson request, [FromServices] IUpdateTopicUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

    }
}
