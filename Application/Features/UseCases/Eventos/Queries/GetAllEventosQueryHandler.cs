using Domain.Interfaces;
using Domain.Models;
using Mapster;
using MediatR;

namespace Application.Features.UseCases.Eventos.Queries
{
    public record GetAllEventosQueryHandler : IRequestHandler<GetAllEventosQuery, List<EventoDTO>>
    {
        private readonly IRepoEvento _repoEvento;
        public GetAllEventosQueryHandler(IRepoEvento repoEvento)
        {
            _repoEvento = repoEvento;
        }
        public async Task<List<EventoDTO>> Handle(GetAllEventosQuery request, CancellationToken cancellationToken)
        {
            var eventos = await _repoEvento.GetAllEventos();
            var lista= eventos.Adapt<List<EventoDTO>>();
            return lista;
        }
    }
}
