using RabbitMQ.Client;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using InMindLab3.Models;

public class RabbitMQPublisherService
{
    private readonly ConnectionFactory _factory;

    public RabbitMQPublisherService()
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
    }

    public async Task PublishLogAsync(LogEntry logEntry)
    {
        logEntry.RequestObject = JsonConvert.SerializeObject(logEntry.RequestData);

        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "log_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = JsonConvert.SerializeObject(logEntry);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: "", routingKey: "log_queue", body: body);

        Console.WriteLine("Log published to RabbitMQ.");
    }
}