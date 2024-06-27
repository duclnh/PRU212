using QuestionRepo.Models;

namespace QuestionRepo.Dto
{
    public class PlantDto
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public DateTime Datetime { get; set; }
        public int CurrentStage { get; set; }
        public int QuantityHarvested { get; set; }
        public string Crop { get; set; }
        /*public int GrowTime { get; set; }
        public int Quantity { get; set; }
        public string Tiles { get; set; }*/
    }
}
