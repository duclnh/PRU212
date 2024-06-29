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
            items = await _context.Items.Where(i => i.UserId == userId && i.Type == type).OrderBy(x => x.SlotId).ToListAsync();
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
                _context.Items.Remove(itemCheck);
                _context.Items.Add(item);
            }
            else
            {
                _context.Items.Add(item);
            }
        }
    }
}
