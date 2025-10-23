
namespace Application.Interfaces.RabbitMQ
{
    public interface IMessageBroker
    {
        public Task<bool> Publish<T>(T message, string QueueName);
    }
}
