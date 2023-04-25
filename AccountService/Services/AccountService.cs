using AccountService.Data;
using ArticleService.IntegrationEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
namespace AccountService.Services
{
    public class AccountService : IAccountService
    {
        private readonly Logger<AccountService> _logger;
        private readonly IModel _model;
        private readonly IUserRepo _userRepo;
        private const string _hostName = "rabbitmq";
        private const string _queueName = "article_account";
        public AccountService(Logger<AccountService> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _model = GetRabbitMqChannel();
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                UserCreateArticleIntegrationEvent myEvent = JsonConvert.DeserializeObject<UserCreateArticleIntegrationEvent>(Encoding.UTF8.GetString(body));
                _logger.LogInformation($"Accept data from rabbitmq: {myEvent.ArticleTitle}");
                _userRepo.CreateNotifications(myEvent);
            };
        }

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

    }
}
