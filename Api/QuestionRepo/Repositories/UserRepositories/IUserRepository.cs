using QuestionRepo.Models;

namespace QuestionRepo.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> UpdateUser(User user);
        Task<User> GetUser(string username);
        Task<User> GetUser(Guid userId);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> IsActive(Guid userId);
        Task<bool> IsActive(string username);
    }
}
