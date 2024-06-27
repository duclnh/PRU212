/*using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.ItemBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _service;
        private readonly IMapper _mapper;
        public ItemController(IItemService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{type}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountRightAnswer>))]
        public async Task<JsonResult> GetItems(Guid userId, string type)
        {
            var items = await _service.GetItems(userId, type);
            var itemsDto = _mapper.Map<List<ItemDto>>(items);
            if (items == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(itemsDto) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<JsonResult> AddItem(Guid userId, [FromBody] List<ItemDto> itemDto)
        {
            var items = _mapper.Map<List<Item>>(itemDto);
            foreach (var item in items)
            {
                item.ItemId = Guid.NewGuid();
                item.UserId = userId;
                await _service.PrepareCreate(item);
            }
            var result = await _service.Save();
            if(result == false)
            {
                return new JsonResult(new { message = "Something went wrong!" }) { StatusCode = StatusCodes.Status400BadRequest };
            }
            return new JsonResult(new { message = "Action Succesful!" }) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
*/