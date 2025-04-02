using SistemaDeRecarga.Context;
using SistemaDeRecarga.Model;
using SistemaDeRecarga.Repository;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeRecarga.Business
{
    public class CursoBusiness : ICursoBusiness
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoBusiness(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<IEnumerable<Curso>> GetAllCursoAsync()
        {
            return await _cursoRepository.GetAllCursoAsync();
        }

        public async Task<Curso> GetCursoByIdAsync(int id)
        {
            return await _cursoRepository.GetCursoByIdAsync(id);
        }

        public async Task<Curso> CreateCursoAsync(Curso curso)
        {
            if (await _cursoRepository.CursoExistAsync(curso.Name))
            {
                throw new Exception("Já existe um curso com este nome");
            }

            if (curso.Id == 0 || curso.Id == null)
            {
                int lastId = await _cursoRepository.GetLastIdAsync();
                curso.Id = lastId + 1;
            }

            await _cursoRepository.CreateCursoAsync(curso);

            return curso;
        }

        public async Task UpdateCursoAsync(Curso curso)
        {
            _cursoRepository.UpdateCursoAsync(curso);
        }

        public async Task DeleteCursoAsync(int id)
        {
            await _cursoRepository.DeleteCursoAsync(id);
        }
    }
}
