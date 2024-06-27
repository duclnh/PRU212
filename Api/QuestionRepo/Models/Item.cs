namespace QuestionRepo.Models
{
    public partial class Item
    {
        public Guid ItemId { get; set; }
        public int SlotId { get; set; }
        public string ItemName { get; set; }
        public string Icon { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; } 
        public string Type { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
