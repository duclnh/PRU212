using QuestionRepo.Models;

namespace QuestionRepo.Business.RecordBusiness
{
    public interface IRecordService
    {
        Task<Record> GetRecord(Guid RecordId);
        Task<IEnumerable<Record>> GetRecords();
        Task<bool> AddRecord(Record Record);
        Task<bool> UpdateRecord(Record Record);
        Task<bool> DeleteRecord(Guid RecordId);
        Task<bool> IsRecordExists(Guid RecordId);

    }
}
