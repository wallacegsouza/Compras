using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Compras.Messaging
{
    public class ConnectionBroker
    {
        private readonly IConfiguration _config;
        public ConnectionFactory ConnFactory { get; private set; }

        public ConnectionBroker (IConfiguration config)
        {
            _config = config;
            string url = _config.GetValue<string>("RABBITMQP_URL");
            ConnFactory = new ConnectionFactory();
            ConnFactory.Uri = new Uri(url);
        }

        public IConnection CreateConnection() {
            return ConnFactory.CreateConnection();
        }

        public void Execute(Action<IModel> eval)
        {
            using (var conn = ConnFactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                eval(channel);
            }
        }

        public T Execute<T>(Func<IModel, T> eval)
        {
            using (var conn = ConnFactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                return eval(channel);
            }
        }
    }
}