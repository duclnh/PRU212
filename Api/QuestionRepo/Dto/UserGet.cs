using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class UserGet
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = null!;

        public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    }
}
