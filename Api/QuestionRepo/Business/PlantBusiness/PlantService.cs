using QuestionRepo.Models;
using QuestionRepo.Repositories.PlantRepositories;

namespace QuestionRepo.Business.PlantBusiness
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;
        public PlantService(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }
        public bool AddPlants(Guid userId, IEnumerable<Plant> plants)
        {
            _plantRepository.DeletePlants(userId);
            _plantRepository.AddPlants(plants);
            var result = _plantRepository.Save();
            return result;
        }

        public async Task<IEnumerable<Plant>> GetPlants(Guid userId)
        {
            return await _plantRepository.GetPlants(userId);
        }

        public void AssignPlants(Guid userId, List<Plant> plants)
        {
            plants.ForEach(x =>
            {
                x.UserId = userId;
                x.PlantId = Guid.NewGuid();
            });
        }
    }
}
