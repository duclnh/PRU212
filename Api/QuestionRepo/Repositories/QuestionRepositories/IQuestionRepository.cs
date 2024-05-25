using QuestionRepo.Models;

namespace QuestionRepo.Repositories.QuestionRepositories
{
    public interface IQuestionRepository
    {
        Task<bool> AddQuestion(Question question);
        Task<bool> DeleteQuestion(int questionId);
        Task<bool> UpdateQuestion(Question question);
        Task<Question> GetQuestion(int questionId);
        Task<IEnumerable<Question>> GetQuestions();
        Task<bool> IsQuestionExists(int questionId);
    }
}
