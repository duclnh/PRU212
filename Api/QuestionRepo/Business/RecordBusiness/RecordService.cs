using QuestionRepo.Models;
using QuestionRepo.Repositories.RecordRepositories;

namespace QuestionRepo.Business.RecordBusiness
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _RecordRepository;

        public RecordService(IRecordRepository RecordRepository)
        {
            _RecordRepository = RecordRepository;
        }

        public async Task<bool> AddRecord(Record Record)
        {
            return await _RecordRepository.AddRecord(Record); ;
        }

        public async Task<bool> DeleteRecord(Guid RecordId)
        {
            return await _RecordRepository.DeleteRecord(RecordId);
        }

        public async Task<Record> GetRecord(Guid RecordId)
        {
            return await _RecordRepository.GetRecord(RecordId);
        }

        public async Task<IEnumerable<Record>> GetRecords()
        {
            return await _RecordRepository.GetRecords();
        }

        public async Task<bool> IsRecordExists(Guid RecordId)
        {
            return await _RecordRepository.IsRecordExists(RecordId);
        }

        public async Task<bool> UpdateRecord(Record Record)
        {
            return await _RecordRepository.UpdateRecord(Record);
        }

    }
}
