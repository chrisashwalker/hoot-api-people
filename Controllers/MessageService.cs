using RabbitMQ.Client;

namespace hoot_api_people.Controllers;

public static class MessageService
{
    private static IConnection connection;
    private static IModel channel;

    static MessageService()
    {
        var factory = new ConnectionFactory { HostName = "hoot-message-queues" };
        MessageService.connection = factory.CreateConnection();
        MessageService.channel = MessageService.connection.CreateModel();
        MessageService.channel.QueueDeclare(queue: "deleted-objects",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
    }

    public static void PostDeletionMessage(string type, long objId)
    {
        byte[] body = System.Text.Encoding.UTF8.GetBytes(new MessageBody(type, objId).ToString());
        MessageService.channel.BasicPublish(exchange: string.Empty,
                                            routingKey: "deleted-objects",
                                            basicProperties: null,
                                            body: body);
    }
}

public class MessageBody {
    public string type;
    public long objId;

    public MessageBody(string type, long objId) 
    {
        this.type = type;
        this.objId = objId;
    }

    public override string ToString() 
    {
        return $"{{\"type\": \"{this.type}\", \"objId\": {objId}}}";
    }
}
