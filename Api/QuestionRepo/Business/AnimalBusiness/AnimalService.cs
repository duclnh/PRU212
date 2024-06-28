using QuestionRepo.Models;
using QuestionRepo.Repositories.AnimalRepositories;

namespace QuestionRepo.Business.AnimalBusiness
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public bool AddAnimals(Guid userId, List<Animal> animals)
        {
            _animalRepository.PrepareRemove(userId);
            _animalRepository.PrepareCreate(animals);
            var result = _animalRepository.Save();
            return result;
        }

        public async Task<List<Animal>> GetAnimals(Guid userId)
        {
            var result = await _animalRepository.GetAnimals(userId);
            return result;
        }

        public void AssignAnimals(Guid userId, List<Animal> animals)
        {
            animals.ForEach(x =>
            {
                x.UserId = userId;
                x.AnimalId = Guid.NewGuid();
            });
        }
    }
}
