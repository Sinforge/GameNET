using ArticleService.DTOs;
using RabbitMQ.Client;

namespace ArticleService.Services
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T message);
    }
}
