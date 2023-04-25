using ArticleService.DTOs;
using RabbitMQ.Client;

namespace ArticleService.Services
{
    public interface IArticleService
    {

        public IModel GetRabbitMqChannel();
        public void CreateArticle(ArticleCreateDto articleCreateDto);
    }
}
