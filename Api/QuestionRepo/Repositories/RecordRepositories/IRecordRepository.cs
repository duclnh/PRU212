using QuestionRepo.Models;

namespace QuestionRepo.Repositories.RecordRepositories
{
    public interface IRecordRepository
    {
        Task<bool> AddRecord(Record record);
        Task<bool> DeleteRecord(int recordId);
        Task<bool> UpdateRecord(Record record);
        Task<Record> GetRecord(int recordId);
        Task<IEnumerable<Record>> GetRecords();
        Task<bool> IsRecordExists(int recordId);
    }
}
