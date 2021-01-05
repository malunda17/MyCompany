using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Common.Logger
{
    public class RabbitLogger : IRabbitLogger, IDisposable
    {
        private readonly IRabbitConfig _rabbitConfig;
        private readonly ILogger<RabbitLogger> _logger;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "logs";
        private bool disposedValue;

        public RabbitLogger(IRabbitConfig rabbitConfig, ILogger<RabbitLogger> logger)
        {
            _rabbitConfig = rabbitConfig ?? throw new ArgumentNullException(nameof(rabbitConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Close();
                    _connection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task LogAsync(LogMessage message)
        {
            await Task.Run(() =>
            {
                var logMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(logMessage);
                _channel.BasicPublish(exchange: ExchangeName, routingKey: "", false, basicProperties: null, body: body);
                _logger.LogInformation("[x] Sent {0}", logMessage);
            });
        }
    }
}