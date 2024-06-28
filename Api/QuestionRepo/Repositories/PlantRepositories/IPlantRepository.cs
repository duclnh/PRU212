using QuestionRepo.Models;

namespace QuestionRepo.Repositories.PlantRepositories
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetPlants(Guid userId);
        void AddPlants(IEnumerable<Plant> plant);
        void DeletePlants(Guid userId);
        bool Save();
    }
}
