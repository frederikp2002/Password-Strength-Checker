using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PasswordHistoryService.Features.Domain.Saga;

public class Orchestrator : BackgroundService
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly HttpClient _httpClient;

    public Orchestrator(HttpClient httpclient)
    {
        _httpClient = httpclient;
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("password_queue", false, false, false, null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(_channel);
            // The orchestrator could poll a RabbitMQ queue for new passwords to process
            // If a new password is available, it could call ProcessPasswordAsync to process it
            // ...

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

}