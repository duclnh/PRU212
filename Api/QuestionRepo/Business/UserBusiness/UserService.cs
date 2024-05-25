using QuestionRepo.Models;
using QuestionRepo.Repositories.UserRepositories;

namespace QuestionRepo.Business.UserBusiness
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User User)
        {
            return await _userRepository.AddUser(User); ;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<User> GetUser(string username)
        {
            return await _userRepository.GetUser(username);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<bool> IsUserExists(int userId)
        {
            return await _userRepository.IsActive(userId);
        }

        public async Task<bool> IsUserExists(string username)
        {
            return await _userRepository.IsActive(username);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}
