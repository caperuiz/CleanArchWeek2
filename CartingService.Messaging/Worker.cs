using CartingService.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Messaging
{
    public class Worker : BackgroundService
    {
        private readonly IRabbitMqService _rabbitMqService;

        public Worker(IServiceProvider services)
        {
            _rabbitMqService = services.GetRequiredService<IRabbitMqService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    //_rabbitMqService.PublishMessage("New scheduled message");
            //    await Task.Delay(5000, stoppingToken);
            //}
        }
    }
}
