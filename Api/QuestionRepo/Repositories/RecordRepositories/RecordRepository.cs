using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddRecord(Record Record)
        {
            _context.Records.Add(Record);
            var isAdded = await _context.SaveChangesAsync();
            return isAdded > 0;
        }

        public async Task<bool> DeleteRecord(int RecordId)
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

        public async Task<Record> GetRecord(int RecordId)
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

        public async Task<bool> IsRecordExists(int RecordId)
        {
            return await _context.Records.AnyAsync(q => q.RecordId == RecordId);
        }

        public async Task<bool> UpdateRecord(Record Record)
        {
            _context.Entry(Record).State = EntityState.Modified;
            var isUpdated = await _context.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
