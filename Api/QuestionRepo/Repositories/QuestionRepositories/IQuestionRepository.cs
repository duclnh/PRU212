using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.QuestionRepositories
{
    public interface IQuestionRepository
    {
        Task<bool> AddQuestion(Question question);
        Task<bool> DeleteQuestion(Guid questionId);
        Task<bool> UpdateQuestion(Question question);
        Task<Question> GetQuestion(Guid questionId);
        Task<IEnumerable<Question>> GetQuestions();
        Task<bool> IsQuestionExists(Guid questionId);
    }
}
