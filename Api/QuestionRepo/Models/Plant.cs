namespace QuestionRepo.Models
{
    public class Plant
    {
        public Guid PlantId { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public DateTime Datetime { get; set; }
        public int CurrentStage { get; set; }
        public int QuantityHarvested { get; set; }
        public string Crop { get; set; }
        public int GrowTime { get; set; }
        public int Quantity { get; set; }
        public string? Tiles { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
