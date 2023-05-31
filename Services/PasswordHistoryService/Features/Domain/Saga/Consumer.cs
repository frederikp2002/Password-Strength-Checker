using System.Diagnostics;
using System.Text;
using System.Text.Json;
using PasswordHistoryService.Features.Application.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PasswordHistoryService.Features.Domain.Saga;

public class Consumer : BackgroundService
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly HttpClient _httpClient;

    public Consumer(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Create a new ConnectionFactory, which will be used to create connections to RabbitMQ.
        // The HostName is set to localhost, indicating that RabbitMQ is running on the same machine.
        var connectionFactory = new ConnectionFactory { HostName = "localhost" };
        // Create a new connection to RabbitMQ using the factory we just created.
        _connection = connectionFactory.CreateConnection();
        // Create a channel within that connection. All the communication with RabbitMQ is done through a channel
        _channel = _connection.CreateModel();

        // Declare a queue to ensure that the queue exists before we try to consume messages from it.
        // The next parameters are: durable, exclusive, auto-delete, and arguments, all set to false or null here.
        _channel.QueueDeclare("password_queue", false, false, false, null);

        Debug.WriteLine("Waiting for messages.");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        // Create a new consumer with the channel we just declared.
        var consumer = new EventingBasicConsumer(_channel);
        // Assign a function to the Received event of the consumer.
        // This function will be called every time the consumer receives a message.
        consumer.Received += (model, ea) =>
        {
            // Get the body of the message from the event arguments.
            // The body is an array of bytes, so we convert it to a string.
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Debug.WriteLine("Received message: " + message);
            // The message handling logic goes here
            try
            {
                var passwordDto = JsonSerializer.Deserialize<CreateRequestDto>(message);
                CreatePassword(passwordDto);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on deserializing: " + e.Message);
                throw;
            }
        };
        // Tell RabbitMQ to deliver messages from the "PasswordStrengthChecked" queue to the consumer.
        // The second parameter is the auto-acknowledge parameter. If set to true, 
        // RabbitMQ will consider messages acknowledged once they have been sent.
        _channel.BasicConsume("password_queue", true, consumer);
        return Task.CompletedTask;
    }

    public override void Dispose() // Is called when the service is stopped
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }

    public async Task CreatePassword(CreateRequestDto password)
    {
        var uri = "https://localhost:44397/api/PasswordHistory/Create";
        var json = JsonSerializer.Serialize(password);

        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
    }
}