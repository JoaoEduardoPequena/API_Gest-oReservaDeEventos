using Domain.Models;
using MediatR;

namespace Application.Features.UseCases.Eventos.Queries
{
    public record GetAllEventosQuery(): IRequest<List<EventoDTO>>;
}
