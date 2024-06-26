using QuestionRepo.Models;

namespace QuestionRepo.Business.ItemBusiness
{
    public interface IItemService
    {
        public Task<bool> AddItem(Item item);
        public Task<List<Item>> GetItems(Guid userId, string type);
    }
}
