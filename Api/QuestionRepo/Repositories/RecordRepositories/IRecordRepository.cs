using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.RecordRepositories
{
    public interface IRecordRepository
    {
        Task<IEnumerable<CountRightAnswer>> GetIQRanking();
        Task<bool> AddRecord(Record record);
        Task<bool> DeleteRecord(Guid recordId);
        Task<bool> UpdateRecord(Record record);
        Task<Record> GetRecord(Guid recordId);
        Task<IEnumerable<Record>> GetRecords();
        Task<bool> IsRecordExists(Guid recordId);
    }
}
