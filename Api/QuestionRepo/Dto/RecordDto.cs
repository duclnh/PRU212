namespace QuestionRepo.Dto
{
    public class RecordDto
    {

        public Guid UserId { get; set; }

        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
    }
}
