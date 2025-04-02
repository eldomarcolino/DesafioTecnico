using SistemaDeRecarga.Model;

public interface ICursoBusiness
{
    Task<IEnumerable<Curso>> GetAllCursoAsync();
    Task<Curso> GetCursoByIdAsync(int id);
    Task<Curso> CreateCursoAsync(Curso curso);
    Task UpdateCursoAsync(Curso curso);
    Task DeleteCursoAsync(int id);
}