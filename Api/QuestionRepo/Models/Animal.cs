namespace QuestionRepo.Models
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public string ItemName { get; set; }
        public DateTime Datetime { get; set; }
        public int CurrentStage { get; set; }
        public int QuantityHarvested { get; set; }
        public int PriceHarvested { get; set; }
        public bool Hungry { get; set; }
        public bool Sick { get; set; }
        public float LocalScaleX { get; set; }
        public float LocalScaleY { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
