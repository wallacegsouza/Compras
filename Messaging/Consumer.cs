using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Compras.Models;
using Compras.Repositories;
using Microsoft.Extensions.Logging;

namespace Compras.Messaging
{
    public class Consumer
    {
        private readonly ConnectionBroker _broker;
        private ILogger<Consumer> _logger;

        public Consumer(
            ConnectionBroker conn,
            ILogger<Consumer> logger)
        {
            _broker = conn;
            _logger = logger;
        }

        public ActionResult Get()
        {
            return _broker.Execute<ActionResult>((channel) => 
            {
                var queueName = "queue-create-compra";
                var data = channel.BasicGet(queueName, false);

                if (data == null)
                    return new EmptyResult();

                var message = Encoding.UTF8.GetString(data.Body.ToArray());
                channel.BasicAck(data.DeliveryTag, false);
                return new JsonResult(JsonSerializer.Deserialize<Compra>(message));
            });
        }
        public void Start(CompraRepository repository)
        {
            _logger.LogInformation("Start Consumer");
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = _broker   .ConnFactory.Uri;
            factory.DispatchConsumersAsync = true;
            // retirado o using resource
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                _logger.LogInformation("Get Message");
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Compra compra = JsonSerializer.Deserialize<Compra>(message);
                var result = await repository.Save(compra); // TODO não esta salvando
                _logger.LogInformation("Retorn Repository.Save: " + result);
                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();
            };
            string tag = channel.BasicConsume("queue-create-compra", false, consumer);
            _logger.LogInformation("BasicConsume tag = " + tag);
        }
    }
}