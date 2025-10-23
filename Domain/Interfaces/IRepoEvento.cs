using Domain.Entites;

namespace Domain.Interfaces
{
    public  interface IRepoEvento
    {
        public Task<bool> CriarEvento(Evento dto);
        public Task<List<Evento>> GetAllEventos();
    }

}
