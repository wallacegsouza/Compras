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
        private readonly ConnectionBroker Broker;
        private CompraRepository Repository;
        private ILogger<Consumer> Logger;

        public Consumer(
            ConnectionBroker conn,
            CompraRepository repository,
            ILogger<Consumer> logger)
        {
            Broker = conn;
            Repository = repository;
            Logger = logger;
        }

        public ActionResult Get()
        {
            return Broker.Execute<ActionResult>((channel) => 
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
        public void Start()
        {
            Logger.LogInformation("Start Consumer");
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = Broker.ConnFactory.Uri;
            factory.DispatchConsumersAsync = true;
            // retirado o using resource
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                Logger.LogInformation("Get Message");
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Logger.LogInformation(message);
                Compra compra = JsonSerializer.Deserialize<Compra>(message);
                Logger.LogInformation("Compra = " + compra);
                await Repository.Save(compra);
                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();
            };
            string tag = channel.BasicConsume("queue-create-compra", false, consumer);
            Logger.LogInformation("BasicConsume tag = " + tag);
        }
    }
}