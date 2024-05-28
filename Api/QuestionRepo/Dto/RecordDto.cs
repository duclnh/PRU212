namespace QuestionRepo.Dto
{
    public class RecordDto
    {

        public Guid UserId { get; set; }

        public string UserAnswer { get; set; } = null!;

        public Guid QuestionId { get; set; }
    }
}
