using QuestionRepo.Models;

namespace QuestionRepo.Repositories.ItemRepositories
{
    public interface IItemRepository
    {
        public Task<List<Item>> GetItems(Guid userId, string type);
        public Task<bool> Save();
        public Task PrepareCreate(Item item);
    }
}
