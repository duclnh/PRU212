/*using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.PlantBusiness;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPlantService _plantService;
        private readonly IMapper _mapper;
        public PlantController(IUserService userService, IPlantService plantService, IMapper mapper)
        {
            _userService = userService;
            _plantService = plantService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlantDto>))]
        public async Task<IActionResult> GetPlants(Guid userId)
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
            var plants = await _plantService.GetPlants(userId);
            var result = _mapper.Map<List<PlantDto>>(plants);
            if (result == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddPlantAsync(Guid userId, [FromBody] List<PlantDto> plants)
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
            var plantsMapping = _mapper.Map<List<Plant>>(plants);
            _plantService.AssignPlants(userId, plantsMapping);
            var result = _plantService.AddPlants(userId, plantsMapping);
            if (!result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            return new JsonResult(null) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
*/