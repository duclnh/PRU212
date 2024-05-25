using QuestionRepo.Models;

namespace QuestionRepo.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateUser(User user);
        Task<User> GetUser(string username);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> IsActive(int userId);
        Task<bool> IsActive(string username);
    }
}
