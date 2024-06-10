using QuestionRepo.Repositories.QuestionRepositories;
using QuestionRepo.Models;
using QuestionRepo.Dto;
using AutoMapper;

namespace QuestionRepo.Business.QuestionBusiness
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddQuestion(QuestionDto question)
        {
            var questionEntity = _mapper.Map<Question>(question);
            return await _questionRepository.AddQuestion(questionEntity); ;
        }

        public async Task<bool> DeleteQuestion(Guid questionId)
        {
            return await _questionRepository.DeleteQuestion(questionId);
        }

        public async Task<QuestionDto> GetQuestion(Guid questionId)
        {
            var questionDto = await _questionRepository.GetQuestion(questionId);
            if (questionDto != null)
            {
                return _mapper.Map<QuestionDto>(questionDto);
            }
            return new QuestionDto();
        }
        public async Task<QuestionDto> RandomQuestion(Guid userId)
        {
            var questionDto = await _questionRepository.RandomQuestion(userId);
            if (questionDto != null)
            {
                return _mapper.Map<QuestionDto>(questionDto);
            }
            return new QuestionDto();
        }
        public async Task<IEnumerable<Question>> GetQuestions()
        {
            return await _questionRepository.GetQuestions();
        }

        public async Task<bool> IsQuestionExists(Guid questionId)
        {
            return await _questionRepository.IsQuestionExists(questionId);
        }

        public async Task<bool> UpdateQuestion(QuestionDto question)
        {
            var questionEntity = _mapper.Map<Question>(question);
            return await _questionRepository.UpdateQuestion(questionEntity);
        }
    }
}
