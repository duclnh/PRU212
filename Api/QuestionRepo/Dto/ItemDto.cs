using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class ItemDto
    {
        public int SlotId { get; set; }
        public string ItemName { get; set; }
        public string Icon { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
    }
}
