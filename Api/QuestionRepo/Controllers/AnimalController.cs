/*using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.AnimalBusiness;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;
        public AnimalController(IUserService userService, IAnimalService animalService, IMapper mapper)
        {
            _userService = userService;
            _animalService = animalService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnimalDto>))]
        public async Task<IActionResult> GetAnimals(Guid userId)
        {
            if(!ModelState.IsValid)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status400BadRequest };
            }
            var user = await _userService.IsUserExists(userId);
            if (!user)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var animals = await _animalService.GetAnimals(userId);
            var result = _mapper.Map<List<AnimalDto>>(animals);
            if (result == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddAnimal(Guid userId, [FromBody] List<AnimalDto> animals)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status400BadRequest };
            }
            var user = await _userService.IsUserExists(userId);
            if (!user)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var animalsMapping = _mapper.Map<List<Animal>>(animals);
            _animalService.AssignAnimals(userId, animalsMapping);
            var result = _animalService.AddAnimals(userId, animalsMapping);
            if(!result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            return new JsonResult(null) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
*/