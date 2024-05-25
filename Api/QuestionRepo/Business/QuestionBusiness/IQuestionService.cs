using QuestionRepo.Models;

namespace QuestionRepo.Business.QuestionBusiness
{
    public interface IQuestionService
    {
        Task<Question> GetQuestion(int questionId);
        Task<IEnumerable<Question>> GetQuestions();
        Task<bool> AddQuestion(Question question);
        Task<bool> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(int questionId);
        Task<bool> IsQuestionExists(int questionId);
    }
}
