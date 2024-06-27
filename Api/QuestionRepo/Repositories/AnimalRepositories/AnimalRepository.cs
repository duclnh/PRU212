using Microsoft.EntityFrameworkCore;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.AnimalRepositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly QuestionWarehouseContext _context;
        public AnimalRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }
        public void PrepareCreate(List<Animal> animals)
        {
            _context.AddRange(animals);
        }

        public async Task<List<Animal>> GetAnimals(Guid userId)
        {
            var result = new List<Animal>();
            result = await _context.Animals.Where(x => x.UserId == userId).ToListAsync();
            return result;
        }

        public void PrepareRemove(Guid userId)
        {
            _context.Animals.RemoveRange(_context.Animals.Where(x => x.UserId == userId));
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
