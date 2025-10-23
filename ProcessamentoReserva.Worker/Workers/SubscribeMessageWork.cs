using ProcessamentoReserva.Worker.Interfaces;

namespace ProcessamentoReserva.Worker.Workers
{
    public class SubscribeMessageWork : BackgroundService
    {
        private readonly ISubscribeMessageProcess _subscribeMessage;
        private readonly ILogger<SubscribeMessageWork> _logger;
        public SubscribeMessageWork(ISubscribeMessageProcess subscribeMessage,ILogger<SubscribeMessageWork> logger)
        {
            _subscribeMessage = subscribeMessage;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
               await _subscribeMessage.SubscribeMessageWork();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while subscribing to messages. Error: {ex.Message}");
            }
           
        }
    }
}
