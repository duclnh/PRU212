namespace QuestionRepo.Dto
{
    public class CountRightAnswer
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public int Count { get; set; }
    }
}
