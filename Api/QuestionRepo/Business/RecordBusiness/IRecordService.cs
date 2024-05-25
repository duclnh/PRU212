using QuestionRepo.Models;

namespace QuestionRepo.Business.RecordBusiness
{
    public interface IRecordService
    {
        Task<Record> GetRecord(int RecordId);
        Task<IEnumerable<Record>> GetRecords();
        Task<bool> AddRecord(Record Record);
        Task<bool> UpdateRecord(Record Record);
        Task<bool> DeleteRecord(int RecordId);
        Task<bool> IsRecordExists(int RecordId);

    }
}
