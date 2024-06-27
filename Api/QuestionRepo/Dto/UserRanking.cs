using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class UserRanking
    {

        public string Username { get; set; } = null!;

        public int Money { get; set; }

        public int Rank { get; set; }

    }
}
