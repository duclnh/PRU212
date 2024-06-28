namespace QuestionRepo.Dto
{
    public class UpdateUser
    {
        public UserInfo UserInfo { get; set; }
        public IEnumerable<ItemDto> ItemsBackpack { get; set; }
        public IEnumerable<ItemDto> ItemsToolbar { get; set; }
        public IEnumerable<AnimalDto> Animals { get; set; }
        public IEnumerable<PlantDto> Plants { get; set; }
    }
}
