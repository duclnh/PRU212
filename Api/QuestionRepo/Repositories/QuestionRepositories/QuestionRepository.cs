﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DeleteQuestion(int questionId)
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

        public async Task<Question> GetQuestion(int questionId)
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


        public async Task<IEnumerable<Question>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<bool> IsQuestionExists(int questionId)
        {
            return await _context.Questions.AnyAsync(q => q.QuestionId == questionId);
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            var isUpdated = await _context.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}