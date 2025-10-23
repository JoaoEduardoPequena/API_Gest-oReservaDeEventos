using FluentValidation;
using Mapster;
using MediatR;
using Domain.Entites;
using Domain.Interfaces;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public class CriarEventoCommandHandler : IRequestHandler<CriarEventoCommand, bool>
    {
        private readonly IRepoEvento _repoEvento;
        private readonly IValidator<CriarEventoCommand> _validator;
        public CriarEventoCommandHandler(IRepoEvento repoEvento, IValidator<CriarEventoCommand> validator)
        {
            _repoEvento = repoEvento;
            _validator = validator;
        }

        public async Task<bool> Handle(CriarEventoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) return false;
            var dto = request.Adapt<Evento>();
            dto.DataEvento = DateTime.Now;
            var result=await _repoEvento.CriarEvento(dto);
            return result;
        }
    }
}
