using MassTransit;

namespace PasswordHistoryService.Features.Domain.Saga;

public class PasswordCheck
{
    public string Password { get; set; } = null!;
    public string PasswordStrength { get; set; } = null!;
}

public class Orchestrator : IConsumer<PasswordCheck>
{
    public async Task Consume(ConsumeContext<PasswordCheck> context)
    {
        var passwordCheck = context.Message;
        Console.WriteLine($"Password: {passwordCheck.Password}");
        Console.WriteLine($"PasswordStrength: {passwordCheck.PasswordStrength}");
    }
}

//public class Orchestrator
//{
//private readonly IModel _channel;
//private readonly IConnection _connection;

//public Orchestrator()
//{
//    // Create a connection factory
//    var factory = new ConnectionFactory { HostName = "localhost" };

//    // Create a connection to RabbitMQ
//    _connection = factory.CreateConnection();

//    // Create a channel to communicate with RabbitMQ
//    _channel = _connection.CreateModel();

//    // Declare an exchange where message will be sent
//    // an exchange is like a mailbox attached to a post office
//    _channel.ExchangeDeclare("PasswordStrengthChecked", ExchangeType.Direct);

//    // Declare a queue where the message will be stored
//    // a queue is like a mailbox
//    var queueName = _channel.QueueDeclare().QueueName;

//    // Bind the queue to the exchange
//    // a routingKey is like a name of a person
//    _channel.QueueBind(queueName, "PasswordStrengthChecked", "PasswordStrengthChecked");

//    // Create a consumer to consume message from the queue
//    var consumer = new EventingBasicConsumer(_channel);

//    // Deserialize the message
//}
//}