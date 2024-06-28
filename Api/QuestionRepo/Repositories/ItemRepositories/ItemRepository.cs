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

        public async Task<List<Item>> GetItems(Guid userId, string type)
        {
            var items = new List<Item>();
            items = await _context.Items.Where(i => i.UserId == userId && i.Type == type).ToListAsync();
            return items;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task PrepareCreate(Item item)
        {
            var itemCheck = await _context.Items.FirstOrDefaultAsync(i => i.SlotId == item.SlotId && i.UserId == item.UserId && i.Type == item.Type);
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
                _context.Items.Add(item);
            }
        }
    }
}
