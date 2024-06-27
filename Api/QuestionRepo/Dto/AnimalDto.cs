using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class AnimalDto
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public int MoveSpeed { get; set; }
        public string NameItem { get; set; }
        public int GrowTime { get; set; }
        public int NumberStage { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public DateTime Datetime { get; set; }
        public int CurrentStage { get; set; }
        public int QuantityHarvested { get; set; }
        public int PriceHarvested { get; set; }
        public bool Hungry { get; set; }
        public bool Sick { get; set; }
    }
}
