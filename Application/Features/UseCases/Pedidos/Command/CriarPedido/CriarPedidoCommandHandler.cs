using FluentValidation;
using MediatR;
using Domain.Interfaces;
using Application.Setting;
using Microsoft.Extensions.Options;
using Application.Interfaces.RabbitMQ;
using Mapster;
using Application.DTO;
using Application.Interfaces;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, bool>
    {
        private readonly IRepoPedido _repoPedido;
        private readonly IValidator<CriarPedidoCommand> _validator;
        private readonly RabbitMqSetting _rabbitMqSetting;
        private readonly IMessageBroker _messageBroker;
        private readonly IRedisService _redisService;
        private readonly RedisSetting _redisSetting;
        public CriarPedidoCommandHandler(IRepoPedido repoPedido, IValidator<CriarPedidoCommand> validator, IOptions<RabbitMqSetting> rabbitMqSetting, IMessageBroker messageBroker, IRedisService redisService)
        {
            _repoPedido = repoPedido;
            _validator = validator;
            _rabbitMqSetting = rabbitMqSetting.Value;
            _messageBroker = messageBroker;
            _redisService = redisService;
        }

        public async Task<bool> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) return false;
            var dto = request.Adapt<MessageReserva>();
            await _redisService.SetAsync<MessageReserva>($"{_redisSetting.BDUsuariosCadastrados}:{request.EmailCliente}", dto, 30, "Days");
            var result=await _messageBroker.Publish<MessageReserva>(dto,_rabbitMqSetting.QueueReserva);
            return result;
        }
    }
}
