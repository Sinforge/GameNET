using ArticleService.Controllers;
using ArticleService.Data;
using ArticleService.DTOs;
using ArticleService.IntegrationEvents;
using ArticleService.Models;
using AutoMapper;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ArticleService.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly ILogger<MessageProducer> _logger;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;


        private const string _hostName = "localhost";
        private const string _userName = "guest", _password = "guest";
        private const string _queueName = "article_account";
        private const string _virtualHost = "/";


        public MessageProducer(ILogger<MessageProducer> logger, IArticleRepo articleRepo, IMapper mapper) {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _logger = logger;   
        }


        public IModel GetRabbitMqChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                VirtualHost = _virtualHost

            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            return channel;
        }

        public void SendMessage<T>(T message)
        {
            var channel = GetRabbitMqChannel();
            var json = JsonSerializer.Serialize(message);
            _logger.LogInformation($"Send data to rabbitmq: {json}");
            byte[] body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: string.Empty,
                     _queueName,
                     basicProperties: null,
                     body: body);

        }
    }
}
