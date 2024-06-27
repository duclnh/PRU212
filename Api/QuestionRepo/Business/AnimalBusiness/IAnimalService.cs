using QuestionRepo.Models;

namespace QuestionRepo.Business.AnimalBusiness
{
    public interface IAnimalService
    {
        public Task<List<Animal>> GetAnimals(Guid userId);
        public bool AddAnimals(Guid userId, List<Animal> animals);
        public void AssignAnimals(Guid userId, List<Animal> animals);
    }
}
