using Microsoft.AspNetCore.Connections;
using MongoDB.Driver.Core.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BasketService.API
{
    public class RabbitMQConsumer
    {
        private readonly IModel _channel;
        private readonly MongoDBContext _mongoDBContext;
        private readonly IConfiguration _configuration;
        public RabbitMQConsumer(IConfiguration configuration, MongoDBContext mongoDBContext)
        {
            _configuration= configuration;
            var host = _configuration["RabbitMQ:HostName"];
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                Port = 5672// _configuration["RabbitMQ:Port"]
            };


            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: configuration["RabbitMQ:QueueName"],
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            _mongoDBContext = mongoDBContext;
        }

        public void ConsumeMessages()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Process the message and save it to MongoDB
                SaveToMongoDB(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "my-queue", autoAck: false, consumer: consumer);
        }
        private void SaveToMongoDB(string message)
        {
            var myModel = new MyModel
            {
                Message = message
            };

            _mongoDBContext.MyCollection.InsertOne(myModel);
        }
    }

}
