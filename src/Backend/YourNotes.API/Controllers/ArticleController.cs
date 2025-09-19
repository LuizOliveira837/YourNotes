using Microsoft.AspNetCore.Mvc;
using YourNotes.API.Attributes;
using YourNotes.Communication.Requests.Article;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.API.Controllers
{
    [ApiController]
    [AuthenticatedUser]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        public ArticleController()
        {

        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseTopicJson), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] RequestArticleJson request, [FromServices] ICreateArticleUseCase useCase)
        {
            await useCase.Execute(request);

            return Created();
        }

    }
}
