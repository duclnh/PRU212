using QuestionRepo.Models;

namespace QuestionRepo.Repositories.AnimalRepositories
{
    public interface IAnimalRepository
    {
        public Task<List<Animal>> GetAnimals(Guid userId);
        public void PrepareCreate(List<Animal> animal);
        public void PrepareRemove(Guid userId);
        public bool Save();

    }
}
