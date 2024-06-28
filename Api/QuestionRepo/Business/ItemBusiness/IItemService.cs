using QuestionRepo.Models;

namespace QuestionRepo.Business.ItemBusiness
{
    public interface IItemService
    {
        public Task PrepareCreate(Item item);
        public Task<bool> Save();
        public Task<List<Item>> GetItems(Guid userId, string type);
    }
}
