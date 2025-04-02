using SistemaDeRecarga.Context;
using SistemaDeRecarga.Model;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeRecarga.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private readonly AppDbContext _context;

        public CursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> GetAllCursoAsync()
        {
            return await _context.Curso.ToListAsync();
        }

        public async Task<Curso> GetCursoByIdAsync(int id)
        {
            return await _context.Curso.FindAsync(id);
        }

        public async Task CreateCursoAsync(Curso curso)
        {
            await _context.Curso.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCursoAsync(Curso curso)
        {
            _context.Curso.Update(curso);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCursoAsync(int id)
        {
            var curso = await _context.Curso.FindAsync(id);

            if (curso != null)
            {
                _context.Curso.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLastIdAsync()
        {
            // Verificar se a tabela está vazia
            if (!await _context.Curso.AnyAsync())
            {
                return 0;
            }

            // Encontrar o maior ID existente no banco de dados
            return await _context.Curso.MaxAsync(x => x.Id);
        }

        public async Task<bool> CursoExistAsync(string name) //Verifica a existencia de um curso no banco
        {
            return await _context.Curso.AnyAsync(x => x.Name == name);
        }
    }
}
