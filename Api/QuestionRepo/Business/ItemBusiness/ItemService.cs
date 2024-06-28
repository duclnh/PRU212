using QuestionRepo.Dto;
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
        public async Task PrepareCreate(Item item)
        {
            await _itemRepository.PrepareCreate(item);
        }
        public async Task<bool> Save()
        {
            return await _itemRepository.Save();
        }
        public async Task<List<Item>> GetItems(Guid userId, string type)
        {
            return await _itemRepository.GetItems(userId, type);
        }
    }
}
