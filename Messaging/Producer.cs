using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Compras.Messaging
{
    public class Producer
    {
        private readonly ConnectionBroker Broker;
        private string QueueCreateCompra = "queue-create-compra";

        public Producer (ConnectionBroker conn)
        {
            Broker = conn;
        }

        public ActionResult Publish(object obj) // TODO: criar um DTO
        {
            Broker.Execute((channel) => 
            {
                var message = JsonSerializer.Serialize(obj);
                var data = Encoding.UTF8.GetBytes(message);
                var exchangeName = "";
                channel.BasicPublish(exchangeName, QueueCreateCompra, null, data);
            });
            return new EmptyResult();
        }

        public void CreateQueue()
        {
            Broker.Execute((channel) => 
            {
                bool durable = true;
                bool exclusive = false;
                bool autoDelete = false;
                channel.QueueDeclare(QueueCreateCompra, durable, exclusive, autoDelete, null);
            });
        }
    }
}