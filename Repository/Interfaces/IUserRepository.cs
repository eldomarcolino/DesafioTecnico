using SistemaDeRecarga.Model;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUserFilterAsync(int? id = null, int? idCourse = null, string username = null, string role = null);
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<int> GetLastIdAsync();
    Task<bool> EmailAndRegistrationNumberExistAsync(string email, string matricula);
    Task<bool> HasUserAsync();
}