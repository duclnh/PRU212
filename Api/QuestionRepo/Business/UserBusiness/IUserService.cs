using QuestionRepo.Models;

namespace QuestionRepo.Business.UserBusiness
{
    public interface IUserService
    {
        Task<User> GetUser(Guid userId);
        Task<User> GetUser(string username);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(Guid userId);
        Task<bool> IsUserExists(Guid userId);
        Task<bool> IsUserExists(string username);
    }
}
