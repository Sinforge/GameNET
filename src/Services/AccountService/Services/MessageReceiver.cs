//using AccountService.Data;
//using ArticleService.IntegrationEvents;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;
//using System.Text.Json;

//namespace AccountService.Services
//{
//    public class MessageReceiver : BackgroundService, IMessageReceiver
//    {
//        private readonly IServiceProvider _sp;


//        private IConnection _connection;
//        private IModel _channel;


//        //TODO : extract to configuration file
//        private const string _hostName = "localhost";
//        private const string _userName = "guest", _password = "guest";
//        private const string _queueName = "article_account";
//        private const string _virtualHost = "/";

//        public MessageReceiver(IServiceProvider sp)
//        {
//            _sp = sp;
//            _channel = GetRabbitMqChannel();
            

//        }

//        public IModel GetRabbitMqChannel()
//        {
//            var factory = new ConnectionFactory
//            {
//                HostName = _hostName,
//                UserName = _userName,
//                Password = _password,
//                VirtualHost = _virtualHost

//            };
//            var connection = factory.CreateConnection();
//            var channel = connection.CreateModel();
//            channel.QueueDeclare(queue: _queueName,
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);
//            return channel;
//        }

//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            if (stoppingToken.IsCancellationRequested)
//            {
//                _channel.Dispose();
//                _connection.Dispose();
//                return Task.CompletedTask;
//            }
//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += (model, ea) =>
//            {
//                var body = ea.Body.ToArray();

//                Task.Run(() =>
//                {
//                    UserCreateArticleEvent myEvent = JsonSerializer.Deserialize<UserCreateArticleEvent>(Encoding.UTF8.GetString(body));
//                    using (var scope = _sp.CreateScope())
//                    {
//                        Console.WriteLine($"We received data {Encoding.UTF8.GetString(body)}");
//                        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepo>(); 
//                        userRepo.CreateNotifications(myEvent);
//                    }
//                });
//            };
//            _channel.BasicConsume(
//                _queueName,
//                true,
//                consumer

//             );
//            return Task.CompletedTask;

//        }
//    }
//}
