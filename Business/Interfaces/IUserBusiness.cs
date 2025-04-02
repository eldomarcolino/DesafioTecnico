using SistemaDeRecarga.Model;

public interface IUserBusiness
{
    Task<IEnumerable<User>> GetAllUserFilterAsync(int? id = null, int? idCourse = null, string username = null, string role = null);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<bool> VerifyUserPasswordAsync(string email, string password);
    Task<User> AuthenticateAsync(string email, string password);
    Task<bool> HasUserAsync();
}