using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.AnimalBusiness;
using QuestionRepo.Business.ItemBusiness;
using QuestionRepo.Business.PlantBusiness;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPlantService _plantService;
        private readonly IAnimalService _animalService;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public ItemController(IUserService userService, IPlantService plantService, IAnimalService animalService, IItemService itemService, IMapper mapper)
        {
            _userService = userService;
            _plantService = plantService;
            _animalService = animalService;
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountRightAnswer>))]
        public async Task<JsonResult> GetItems(Guid userId)
        {
            var isExists = await _userService.IsUserExists(userId);
            if (!isExists)
            {
                return new JsonResult(new { data = (object?)null, message = "User is not exists.", status = 404 }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var plants = await _plantService.GetPlants(userId);
            var plantsDto = _mapper.Map<List<PlantDto>>(plants);

            var animals = await _animalService.GetAnimals(userId);
            var animalsDto = _mapper.Map<List<AnimalDto>>(animals);

            var itemsBackpack = await _itemService.GetItems(userId, "backpack");
            var backpacks = _mapper.Map<List<ItemDto>>(itemsBackpack);

            var itemsToolbar = await _itemService.GetItems(userId, "toolbar");
            var toolbars = _mapper.Map<List<ItemDto>>(itemsToolbar);

            if (plantsDto == null || animalsDto == null || backpacks == null || toolbars == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            var allItems = new AllItem
            {
                Plants = plantsDto,
                Animals = animalsDto,
                ItemsBackpack = backpacks,
                ItemsToolbar = toolbars
            };

            return new JsonResult((object)allItems) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
