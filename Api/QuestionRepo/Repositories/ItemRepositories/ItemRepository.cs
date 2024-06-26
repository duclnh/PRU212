using Microsoft.EntityFrameworkCore;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.ItemRepositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly QuestionWarehouseContext _context;
        public ItemRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddItem(Item item)
        {
            var itemCheck = await _context.Items.SingleAsync(i => i.UserId == item.UserId);
            if (itemCheck != null)
            {
                itemCheck.ItemName = item.ItemName;
                itemCheck.Type = item.Type;
                itemCheck.Price = item.Price;
                itemCheck.Amount = item.Amount;
                itemCheck.Icon = item.Icon;
            }
            else
            {
                await _context.Items.AddAsync(item);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Item>> GetItems(Guid userId, string type)
        {
            var Items = await _context.Items.Where(i => i.UserId == userId && i.Type == type).ToListAsync();
            return Items;
        }
    }
}
