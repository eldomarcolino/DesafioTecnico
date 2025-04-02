using SistemaDeRecarga.Model;
using SistemaDeRecarga.Repository;

namespace SistemaDeRecarga.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUserFilterAsync(int? id = null, int? idCourse = null, string username = null, string role = null)
        {
            return await _userRepository.GetAllUserFilterAsync(id, idCourse, username, role);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (await _userRepository.EmailAndRegistrationNumberExistAsync(user.Email, user.RegistrationNumber))
            {
                throw new Exception("Este email ou esta matrícula já foi registrada");
            }

            //Criptografar a senha
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            if (user.Id == 0 || user.Id == null)
            {
                int lastId = await _userRepository.GetLastIdAsync();
                user.Id = lastId + 1;
            }

            await _userRepository.CreateUserAsync(user);

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (!string.IsNullOrEmpty(user.Password) && user.Password != existingUser.Password)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            else
            {
                user.Password = existingUser.Password;
            }

            _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> VerifyUserPasswordAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> HasUserAsync()
        {
           return await _userRepository.HasUserAsync();
        }
    }
}
