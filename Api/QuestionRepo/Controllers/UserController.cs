using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.AnimalBusiness;
using QuestionRepo.Business.ItemBusiness;
using QuestionRepo.Business.PlantBusiness;
using QuestionRepo.Business.RecordBusiness;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;
using System.Numerics;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IPlantService _plantService;
        private readonly IAnimalService _animalService;
        private readonly IItemService _itemService;
        private readonly IRecordService _recordService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, IMapper mapper, IAnimalService animalService, IPlantService plantService, IItemService itemService, IRecordService recordService, ILogger<UserController> logger)
        {
            _service = service;
            _mapper = mapper;
            _animalService = animalService;
            _plantService = plantService;
            _itemService = itemService;
            _recordService = recordService;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet("ranking/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserRanking>))]
        public async Task<JsonResult> GetUsers(Guid userId)
        {
            var Users = await _service.GetUsers();
            if (Users == null)
            {
                return new JsonResult(new {message = "Something went wrong. Please come back later."}) { StatusCode = StatusCodes.Status404NotFound };
            }
            #region Rank for User Money
            var usersDto = _mapper.Map<IEnumerable<UserRanking>>(Users);
            var count = 1;
            foreach (var user in usersDto)
            {
                user.RankMoney = count++;
            }
            var currentUser = await _service.GetUserCurrentRank(userId);
            #endregion

            #region Rank for User Right Answer
            List<CountRightAnswer> records = (List<CountRightAnswer>)await _recordService.GetIQRanking();
            var countAnswer = 1;
            foreach (var record in records)
            {
                record.Rank = $"{countAnswer++}/{records.Count()}";
            }
            #endregion

            var currentRank = new CurrentRank
            {
                userRankings = usersDto,
                countRightAnswer = records,
                currentRankMoney = $"{currentUser.RankMoney}/{usersDto.Count()}",
                money = currentUser.Money,
                currentRankIQ = records.FirstOrDefault(q => q.UserId == userId)?.Rank ?? $"0/{records.Count()}",
                rightAnswer = records.FirstOrDefault(q => q.UserId == userId)?.Count ?? 0
            };

            return new JsonResult(currentRank) { StatusCode = StatusCodes.Status200OK };
        }

        // GET: api/Users/5
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserLogin))]
        public async Task<JsonResult> GetUser(Guid userId)
        {
            var isExists = await _service.IsUserExists(userId);
            if (!isExists)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var User = _mapper.Map<UserLogin>(await _service.GetUser(userId));
            return new JsonResult(User) { StatusCode = StatusCodes.Status200OK };
        }


        // GET: api/Users/5
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(UserInfo))]
        public async Task<JsonResult> GetUser([FromBody] UserLogin userLogin)
        {
            var isExists = await _service.IsUserExists(userLogin.Username);
            if (!isExists)
            {
                return new JsonResult(new {data = (object?)null, message = "User is not exists.",status = 404}) { StatusCode = StatusCodes.Status404NotFound };
            }

            var user = await _service.GetUser(userLogin.Username);
            var userInfo = _mapper.Map<UserInfo>(user);
            if (userLogin.Password != user.Password)
            {
                return new JsonResult(new { data = (object?)null, message = "Password is invalid.", status = 404 }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(new { data = (object)userInfo, message = "Login successful!", status = 200 }) { StatusCode = StatusCodes.Status200OK };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<JsonResult> PutUser(Guid userId, [FromBody] UpdateUser userToUpdate)
        {
                if (userToUpdate == null)
            {
                return new JsonResult(new { message = "User Information is required!" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!await _service.IsUserExists(userId))
            {
                return new JsonResult(new { message = "User Information is not exist!" }) { StatusCode = StatusCodes.Status404NotFound };
            }

            /*var users = await _service.GetUsers();
            var isConflict = users.Any(u => u.Username == userToUpdate.Username && u.UserId != userId);
            if(isConflict)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status409Conflict };
            }*/
            if(userId != userToUpdate.UserInfo.UserId)
            {
                return new JsonResult(new { message = "UserId is not matching!" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
            }

            // Update Items
            var items = _mapper.Map<List<Item>>(userToUpdate.ItemsBackpack);
            items.AddRange(_mapper.Map<List<Item>>(userToUpdate.ItemsToolbar));
            foreach (var item in items)
            {
                item.ItemId = Guid.NewGuid();
                item.UserId = userId;
                await _itemService.PrepareCreate(item);
            }
            var addItem = await _itemService.Save();
            if (addItem == false)
            {
                return new JsonResult(new { message = "Something went wrong!" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            // Update Plants
            var plants = _mapper.Map<List<Plant>>(userToUpdate.Plants);
            if(plants.Count > 0)
            {
                _plantService.AssignPlants(userId, plants);
                var addPlant = _plantService.AddPlants(userId, plants);
            }

            // Update Animals
            var animals = _mapper.Map<List<Animal>>(userToUpdate.Animals);
            if(animals.Count > 0)
            {
                _animalService.AssignAnimals(userId, animals);
                var result = _animalService.AddAnimals(userId, animals);
            }

            // Update User
            var user = await _service.GetUser(userId);
            user.Money = userToUpdate.UserInfo.Money;
            user.PositionX = userToUpdate.UserInfo.PositionX;
            user.PositionY = userToUpdate.UserInfo.PositionY;
            user.PositionZ = userToUpdate.UserInfo.PositionZ;
            user.Sence = userToUpdate.UserInfo.Sence;
            var isUpdated = !await _service.UpdateUser(user);
            return new JsonResult(new { message = "Saved Successfully!." }) { StatusCode = StatusCodes.Status200OK };
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<JsonResult> PostUser([FromBody] UserLogin userCreate)
        {
            if (userCreate == null)
            {
                return new JsonResult(new { data = (object?)null, message = "Username and password is required", status = 400 }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var users = await _service.GetUsers();
            var user = users.FirstOrDefault(q => q.Username.Trim().ToUpper() == userCreate.Username.Trim().ToUpper());
            if (user != null)
            {
                return new JsonResult(new { data = (object?)null, message = "Username already exists.", status = 422 }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value?.Errors.Select(e => e.ErrorMessage).ToList());

                return new JsonResult(new { data = (object?)null, message = "Invalid username and password", status = 400 }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var userMap = _mapper.Map<User>(userCreate);
            userMap.UserId = Guid.NewGuid();
            if (!_service.AddUser(userMap).Result)
            {

                return new JsonResult(new { data = (object?)null, message = "Failed to register.", status = 500 }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new JsonResult(new { data = (object?)null, message = "Register successfully!", status = 200 }) { StatusCode = StatusCodes.Status200OK };
        }

        // DELETE: api/Users/5
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> DeleteUser(Guid userId)
        {
            if (!await _service.IsUserExists(userId))
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!await _service.DeleteUser(userId))
            {
                return new JsonResult(new { message = "Something went wrong deleting User" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new JsonResult(null) { StatusCode = StatusCodes.Status204NoContent };
        }
    }
}
