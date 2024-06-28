using Microsoft.EntityFrameworkCore;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QuestionWarehouseContext _context;

        public UserRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(User user)
        {
            user.Money = 700;
            _context.Users.Add(user);
            var isAdded = await _context.SaveChangesAsync();
            return isAdded > 0;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserRanking> GetUserCurrentRank(Guid userId)
        {
            if (_context.Users == null)
            {
                return null;
            }
            List<User> users = (List<User>)await GetUsers();
            var currentUser = users.FirstOrDefault(u => u.UserId == userId);
            if (currentUser == null)
            {
                return null;
            }
            int currentIndex = users.IndexOf(currentUser) + 1;
            var userRanking = new UserRanking
            {
                Username = currentUser.Username,
                Money = currentUser.Money,
                RankMoney = currentIndex
            };

            return userRanking;
        }

        public async Task<User> GetUser(string username)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => username.Equals(u.Username));

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> GetUser(Guid userId)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => userId == u.UserId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.OrderByDescending(u => u.Money).ThenByDescending(u => u.Username).ToListAsync();
        }
        public async Task<bool> IsActive(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<bool> IsActive(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            /*if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;
            }*/
            var isUpdated = await _context.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
