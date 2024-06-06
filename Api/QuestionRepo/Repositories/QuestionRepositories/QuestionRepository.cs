using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.QuestionRepositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionWarehouseContext _context;

        public QuestionRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            var isAdded = await _context.SaveChangesAsync();
            return isAdded > 0;
        }

        public async Task<bool> DeleteQuestion(Guid questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                return false;
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Question> GetQuestion(Guid questionId)
        {
            if (_context.Questions == null)
            {
                return null;
            }
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                return null;
            }

            return question;
        }

        public async Task<Question> RandomQuestion(Guid userId)
        {
            var randomQuestion = await _context.Questions
                                        .Where(q => !q.Records.Any(r => r.UserId == userId))
                                        .OrderBy(q => Guid.NewGuid())
                                        .FirstOrDefaultAsync();
            if (randomQuestion != null)
            {
                return randomQuestion;
            }
            return null;
        }

        public async Task<IEnumerable<Question>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<bool> IsQuestionExists(Guid questionId)
        {
            return await _context.Questions.AnyAsync(q => q.QuestionId == questionId);
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            var existingQuestion = await _context.Questions.FindAsync(question.QuestionId);
            if (existingQuestion != null)
            {
                _context.Entry(existingQuestion).CurrentValues.SetValues(question);
            }
            else
            {
                _context.Entry(question).State = EntityState.Modified;
            }
            var isUpdated = await _context.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
