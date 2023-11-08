using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BasketService.API
{
    public class RabbitMQConsumer
    {
        private readonly IModel _channel;
        private readonly MongoDBContext _mongoDBContext;
        public RabbitMQConsumer(IConfiguration configuration, MongoDBContext mongoDBContext)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: configuration["RabbitMQ:QueueName"], durable: false, exclusive: false, autoDelete: false, arguments: null);
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
