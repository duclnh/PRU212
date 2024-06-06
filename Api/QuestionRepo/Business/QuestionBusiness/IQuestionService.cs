using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Business.QuestionBusiness
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestion(Guid questionId);
        Task<QuestionDto> RandomQuestion(Guid userId);
        Task<IEnumerable<Question>> GetQuestions();
        Task<bool> AddQuestion(QuestionDto question);
        Task<bool> UpdateQuestion(QuestionDto question);
        Task<bool> DeleteQuestion(Guid questionId);
        Task<bool> IsQuestionExists(Guid questionId);
    }
}
