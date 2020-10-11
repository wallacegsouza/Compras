using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Compras.Messaging
{
    public class Producer
    {
        private readonly ConnectionBroker _broker;
        private readonly ILogger _logger;

        private string QueueCreateCompra = "queue-create-compra";

        public Producer (ConnectionBroker conn, ILogger<Producer> logger)
        {
            _broker = conn;
            _logger = logger;
        }

        public ActionResult Publish(object obj) // TODO: criar um DTO
        {
            _logger.LogInformation("Publish Message");
            _broker.Execute((channel) =>
            {
                var message = JsonSerializer.Serialize(obj);
                _logger.LogInformation("Message: " + message);
                var data = Encoding.UTF8.GetBytes(message);
                var exchangeName = "";
                channel.BasicPublish(exchangeName, QueueCreateCompra, null, data);
            });
            return new EmptyResult();
        }

        public void CreateQueue()
        {
            _broker.Execute((channel) => 
            {
                bool durable = true;
                bool exclusive = false;
                bool autoDelete = false;
                channel.QueueDeclare(QueueCreateCompra, durable, exclusive, autoDelete, null);
            });
        }
    }
}