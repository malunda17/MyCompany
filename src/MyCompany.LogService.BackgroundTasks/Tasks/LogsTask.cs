using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyCompany.Common.Logger;
using MyCompany.LogService.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.LogService.BackgroundTasks.Tasks
{
    public class LogsTask : BackgroundService
    {
        private readonly ILogRepository _logRepository;
        private readonly IRabbitConfig _rabbitConfig;
        private readonly ILogger<LogsTask> _logger;
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private const string ExchangeName = "logs";
        private string _queueName;

        public LogsTask(ILogRepository logRepository, IRabbitConfig rabbitConfig, ILogger<LogsTask> logger)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _rabbitConfig = rabbitConfig ?? throw new ArgumentNullException(nameof(rabbitConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitConfig.HostName,
                Port = _rabbitConfig.Port,
                UserName = _rabbitConfig.UserName,
                Password = _rabbitConfig.Password
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

            _queueName = _channel.QueueDeclare(exclusive: false).QueueName;
            _channel.QueueBind(queue: _queueName, exchange: ExchangeName, routingKey: "");
            _logger.LogInformation(" [*] Waiting for logs");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                var log = JsonConvert.DeserializeObject<Log>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                _logRepository.AddAsync(log);
                _logger.LogInformation(" [x] {0}", Encoding.UTF8.GetString(ea.Body.ToArray()));
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, _consumer);

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            _connection.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}