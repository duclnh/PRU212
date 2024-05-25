using QuestionRepo.Models;

namespace QuestionRepo.Business.UserBusiness
{
    public interface IUserService
    {
        Task<User> GetUser(string username);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> AddUser(User User);
        Task<bool> UpdateUser(User User);
        Task<bool> DeleteUser(int UserId);
        Task<bool> IsUserExists(int userId);
        Task<bool> IsUserExists(string username);
    }
}
