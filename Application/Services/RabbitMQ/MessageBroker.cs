using Application.Interfaces.RabbitMQ;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Services.RabbitMQ
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IBus _IBus;
        private readonly ILogger<MessageBroker> _logger;
        public MessageBroker(IBus bus, ILogger<MessageBroker> logger)
        {
            _IBus = bus;
            _logger = logger;
        }

        public async Task<bool> Publish<T>(T message, string QueueName)
        {
            try
            {
                var uri = new Uri($"queue:{QueueName}");
                var iBus = await _IBus.GetSendEndpoint(uri);
                await iBus.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                var errMsg = $"Erro ao publicar mensagem na fila queue {QueueName}: {ex.Message}";
                _logger.LogError(errMsg);
                return false;
            }
        }
       
    }
}
