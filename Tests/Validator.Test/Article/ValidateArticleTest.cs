using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Article;
using YourNotes.Communication.Requests.Article;
using YourNotes.Exception;

namespace Validators.Test.Article
{
    public class ValidateArticleTest
    {

        [Fact]
        public void Sucess()
        {
            //arrange
            var validator = new ValidateArticle();
            var article = ArticleBuilder.Build(null);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = article.Title,
                TopicId = article.TopicId,
            };

            //act
            var result = validator.Validate(request);


            //assert

            result
                .IsValid
                .Should().BeTrue();
        }

        [Fact]
        public void ERRO_Invalid_Description()
        {
            //arrange
            var validator = new ValidateArticle();
            var article = ArticleBuilder.Build(null);
            var request = new RequestArticleJson()
            {
                Description = string.Empty,
                Title = article.Title,
                TopicId = article.TopicId,
            };

            //act
            var result = validator.Validate(request);


            //assert

            result
                .IsValid
                .Should().BeFalse();

            result
                .Errors
                .Count()
                .Should().Be(1);

            result
                .Errors
                .Any(e=> e.ErrorMessage == YourNotesExceptionResource.INVALID_DESCRIPTION_LENGTH)
                .Should().BeTrue();
        }

        [Fact]
        public void ERRO_Invalid_Title()
        {
            //arrange
            var validator = new ValidateArticle();
            var article = ArticleBuilder.Build(null);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = string.Empty,
                TopicId = article.TopicId,
            };

            //act
            var result = validator.Validate(request);


            //assert

            result
                .IsValid
                .Should().BeFalse();

            result
                .Errors
                .Count()
                .Should().Be(1);

            result
                .Errors
                .Any(e => e.ErrorMessage == YourNotesExceptionResource.INVALID_ARTICLE_LENGTH)
                .Should().BeTrue();
        }
    }
}
