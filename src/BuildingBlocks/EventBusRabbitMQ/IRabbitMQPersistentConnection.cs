using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    internal interface IRabbitMQPersistentConnection : IDisposable
    {
        IModel CreateModel();

        bool TryConnect();

        bool IsConnected { get; }
    
    }
}
