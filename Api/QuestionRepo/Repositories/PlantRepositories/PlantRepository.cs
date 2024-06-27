using Microsoft.EntityFrameworkCore;
using QuestionRepo.Models;

namespace QuestionRepo.Repositories.PlantRepositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly QuestionWarehouseContext _context;
        public PlantRepository(QuestionWarehouseContext context)
        {
            _context = context;
        }

        public void AddPlants(IEnumerable<Plant> plants)
        {
            if(plants.Any())
            {
                _context.Plants.AddRange(plants);
            }
        }

        public void DeletePlants(Guid userId)
        {
            try
            {
                var plants = _context.Plants.Where(p => p.UserId == userId);
                if(plants.Any())
                {
                       _context.Plants.RemoveRange(plants);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Plant>> GetPlants(Guid userId)
        {
            var result = new List<Plant>();
            result = await _context.Plants.Where(p => p.UserId == userId).ToListAsync();
            return result;
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
