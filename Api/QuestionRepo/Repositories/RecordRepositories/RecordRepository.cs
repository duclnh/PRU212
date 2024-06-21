using Microsoft.EntityFrameworkCore;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.RecordRepositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly QuestionWarehouseContext _context;

        public RecordRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRecord(Record record)
        {
            /*_context.Records.Add(Record);
            var isAdded = await _context.SaveChangesAsync();
            return isAdded > 0;*/
            /*var query = $@"
                        DECLARE @userId UNIQUEIDENTIFIER= {userId};
                        DECLARE @questionId UNIQUEIDENTIFIER = {questionId};
                        DECLARE @userAnswer NVARCHAR(1) = {answer};
                        EXEC [dbo].[CreateRecord] @userId, @questionId, @userAnswer;
                        ";*/
            _context.Records.Add(record);
            var isAdded = await _context.SaveChangesAsync();
            return isAdded > 0;
        }

        public async Task<bool> DeleteRecord(Guid RecordId)
        {
            var Record = await _context.Records.FindAsync(RecordId);
            if (Record == null)
            {
                return false;
            }
            _context.Records.Remove(Record);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Record> GetRecord(Guid RecordId)
        {
            if (_context.Records == null)
            {
                return null;
            }
            var Record = await _context.Records.FindAsync(RecordId);

            if (Record == null)
            {
                return null;
            }

            return Record;
        }


        public async Task<IEnumerable<Record>> GetRecords()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<IEnumerable<CountRightAnswer>> GetIQRanking()
        {
            return await _context.Records
            .Where(r => r.IsCorrect)
            .GroupBy(r => r.UserId)
            .Select(g => new CountRightAnswer
            {
                UserId = g.Key,
                Username = _context.Users.FirstOrDefault(u => u.UserId == g.Key).Username,
                Count = g.Count()
            })
            .Take(10)
            .ToListAsync();
        }

        public async Task<bool> IsRecordExists(Guid RecordId)
        {
            return await _context.Records.AnyAsync(q => q.RecordId == RecordId);
        }

        public async Task<bool> UpdateRecord(Record Record)
        {
            var existingRecord = await _context.Records.FindAsync(Record.RecordId);
            if (existingRecord != null)
            {
                _context.Entry(existingRecord).CurrentValues.SetValues(Record);
            }
            else
            {
                _context.Entry(Record).State = EntityState.Modified;
            }
            var isUpdated = await _context.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
