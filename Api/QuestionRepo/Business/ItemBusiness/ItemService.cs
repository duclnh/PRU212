using QuestionRepo.Models;
using QuestionRepo.Repositories.ItemRepositories;

namespace QuestionRepo.Business.ItemBusiness
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public Task<bool> AddItem(Item item)
        {
            return _itemRepository.AddItem(item);
        }

        public Task<List<Item>> GetItems(Guid userId, string type)
        {
            return _itemRepository.GetItems(userId, type);
        }
    }
}
