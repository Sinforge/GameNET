using ArticleService.Controllers;
using ArticleService.Data;
using ArticleService.DTOs;
using ArticleService.IntegrationEvents;
using ArticleService.Models;
using AutoMapper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ArticleService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ILogger<ArticleService> _logger;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;
        private readonly IModel _channel;

        public ArticleService(ILogger<ArticleService> logger, IArticleRepo articleRepo, IMapper mapper) {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _logger = logger;   
            _channel = GetRabbitMqChannel();
        }
        private const string _hostName = "rabbitmq";
        private const string _queueName = "article_account";


        public IModel GetRabbitMqChannel()
        {
            var factory = new ConnectionFactory { HostName = _hostName };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            return channel;
        }
        public void CreateArticle(ArticleCreateDto articleCreateDto)
        {
            var article = _mapper.Map<Article>(articleCreateDto);
            _articleRepo.CreateArticle(article);
            _articleRepo.SaveChanges();
            UserCreateArticleIntegrationEvent userCreateArticleIntegrationEvent = new UserCreateArticleIntegrationEvent(article.Id, article.Title, articleCreateDto.Owner);
            string json = JsonConvert.SerializeObject(userCreateArticleIntegrationEvent);
            _logger.LogInformation($"Send data to rabbitmq: {json}");

            byte[] eventBuffer = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: string.Empty,
                     routingKey: _queueName,
                     basicProperties: null,
                     body: eventBuffer);
        }
    }
}
