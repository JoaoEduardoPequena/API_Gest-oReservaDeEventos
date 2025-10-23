using Infrastruture.Worker.DTO;
using Infrastruture.Worker.Interfaces;
using Infrastruture.Worker.Setting;
using Microsoft.Extensions.Options;
using ProcessamentoReserva.Worker.Interfaces;

namespace ProcessamentoReserva.Worker.Process
{
    public class SubscribeMessageProcess : ISubscribeMessageProcess
    {
        private readonly ISendEmailService _emailService;
        private readonly EmailSetting _emailSetting;
        private readonly IRedisService _redisService;
        private readonly RedisSetting _redisSetting;
        public SubscribeMessageProcess(ISendEmailService emailService, IOptions<EmailSetting> emailSetting,IRedisService redisService,IOptions<RedisSetting> redisSetting)
        {
            _emailService = emailService;
            _emailSetting = emailSetting.Value;
            _redisService = redisService;
            _redisSetting = redisSetting.Value;
        }

        public async Task SubscribeMessageWork()
        {
            /*var fullChannel = $"{_redisSetting.Channel}";
            _redisService.Subscribe<MessageDTO>(fullChannel, async (redisMessage) =>
            {
                if (redisMessage is null) return;
                var dtoEmail = new EmailDTO();
                dtoEmail.To = redisMessage.Email;
                dtoEmail.EmailSubject = _emailSetting.EmailSubject;
                dtoEmail.EmailText = redisMessage.Message;
                await _emailService.SendEmailAsync(dtoEmail);
            });*/

            await Task.CompletedTask;
        }
    }
}
