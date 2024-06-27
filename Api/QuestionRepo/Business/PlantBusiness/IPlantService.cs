using QuestionRepo.Models;

namespace QuestionRepo.Business.PlantBusiness
{
    public interface IPlantService
    {
        public Task<IEnumerable<Plant>> GetPlants(Guid userId);
        public bool AddPlants(Guid userId, IEnumerable<Plant> plants);
        public void AssignPlants(Guid userId, List<Plant> plants);
    }
}
