using Infrastruture.Worker.DTO;
using Infrastruture.Worker.Interfaces;
using Infrastruture.Worker.Setting;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ProcessamentoReserva.Worker.Consumers
{
    public class PedidosReservasConsumer : IConsumer<MessageReserva>
    {
       
        private readonly ISendEmailService _emailService;
        private readonly EmailSetting _emailSetting;
        private readonly ILogger<PedidosReservasConsumer> _logger;

        public PedidosReservasConsumer(IRedisService redisService,IOptions<RedisSetting> redisSetting,IOptions<EmailSetting>  emailSetting, ISendEmailService emailService, ILogger<PedidosReservasConsumer> logger)
        {
            _emailSetting= emailSetting.Value;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MessageReserva> context)
        {
            try
            {
                if (context.Message is not null)
                {
                    var dto = new EmailDTO();
                    dto.EmailSubject = _emailSetting.EmailSubject;
                    dto.User= context.Message.NomeCliente;
                    dto.To = context.Message.EmailCliente;
                    dto.EmailText = $"Prezado(a) {context.Message.NomeCliente}, sua reserva para o evento {context.Message.Id_Evento} em {context.Message.DataReserva} foi confirmada.";
                    var res = await _emailService.SendEmailAsync(dto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consumir mensagem do consumer PedidosReservasConsumer: {ex.Message}");
            }
        }
    }
}
