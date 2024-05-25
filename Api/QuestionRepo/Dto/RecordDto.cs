namespace QuestionRepo.Dto
{
    public class RecordDto
    {
        public int RecordId { get; set; }

        public int UserId { get; set; }

        public string UserAnswer { get; set; } = null!;

        public int QuestionId { get; set; }
    }
}
