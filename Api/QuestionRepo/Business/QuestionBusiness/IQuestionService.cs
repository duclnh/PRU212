using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Business.QuestionBusiness
{
    public interface IQuestionService
    {
        Task<Question> GetQuestion(Guid questionId);
        Task<QuestionDto> RandomQuestion(Guid userId);
        Task<IEnumerable<Question>> GetQuestions();
        Task<bool> AddQuestion(Question question);
        Task<bool> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(Guid questionId);
        Task<bool> IsQuestionExists(Guid questionId);
    }
}
