using Domain.Entites;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories
{
    public class RepoEvento : IRepoEvento
    {
        private readonly ApplicationDbContext _context;
        public RepoEvento(ApplicationDbContext context)
        {
            _context = context;
        }
   
        public async Task<bool> CriarEvento(Evento dto)
        {   
            try
            {
                await _context.Evento.AddAsync(dto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Evento>> GetAllEventos()
        {
           return await _context.Evento.
                     ToListAsync();
        }
    }
}
