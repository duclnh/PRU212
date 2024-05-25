using QuestionRepo.Repositories.QuestionRepositories;
using QuestionRepo.Models;

namespace QuestionRepo.Business.QuestionBusiness
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> AddQuestion(Question question)
        {
            return await _questionRepository.AddQuestion(question); ;
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            return await _questionRepository.DeleteQuestion(questionId);
        }

        public async Task<Question> GetQuestion(int questionId)
        {
            return await _questionRepository.GetQuestion(questionId);
        }

        public async Task<IEnumerable<Question>> GetQuestions()
        {
            return await _questionRepository.GetQuestions();
        }

        public async Task<bool> IsQuestionExists(int questionId)
        {
            return await _questionRepository.IsQuestionExists(questionId);
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            return await _questionRepository.UpdateQuestion(question);
        }
    }
}
