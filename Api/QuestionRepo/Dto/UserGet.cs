using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class UserGet
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = null!;

    }
}
