using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourNotes.API.Attributes;
using YourNotes.Application.Topic.CreateTopic;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;

namespace YourNotes.API.Controllers
{
    [Route("[controller]")]
    [AuthenticatedUser]
    [ApiController]
    public class TopicController : Controller
    {

        public TopicController()
        {

        }


        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] RequestTopicJson request, [FromServices] ICreateTopicUseCase useCase)
        {
           var id = await useCase.Execute(request);

            return Created("", id);
        }

    }
}
