using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGet>))]
        public async Task<JsonResult> GetUsers()
        {
            var Users = await _service.GetUsers();
            if (Users == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var usersDto = _mapper.Map<IEnumerable<UserGet>>(Users);
            return new JsonResult(usersDto) { StatusCode = StatusCodes.Status200OK };
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
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<JsonResult> GetUser([FromBody] UserLogin user)
        {
            var isExists = await _service.IsUserExists(user.Username);
            if (!isExists)
            {
                return new JsonResult(new {data = (object?)null, message = "User is not exists.",status = 404}) { StatusCode = StatusCodes.Status404NotFound };
            }
            var userGet = _mapper.Map<User>(await _service.GetUser(user.Username));
            if (user.Password != userGet.Password)
            {
                return new JsonResult(new { data = (object?)null, message = "Password is invalid.", status = 404 }) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(new { data = (object)userGet, message = "Login successful!", status = 200 }) { StatusCode = StatusCodes.Status200OK };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<JsonResult> PutUser(Guid userId, [FromBody] UserLogin userToUpdate)
        {
            if (userToUpdate == null)
            {
                return new JsonResult(new { message = "User is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.IsUserExists(userId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            var users = _service.GetUsers().Result;
            var isConflict = users.Any(u => u.Username == userToUpdate.Username && u.UserId != userId);
            if(isConflict)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status409Conflict };
            }

            var user = await _service.GetUser(userId);
            user.Username = userToUpdate.Username;
            user.Password = userToUpdate.Password;
            var isUpdated = !_service.UpdateUser(user).Result;
            if (isUpdated)
            {
                return new JsonResult(new { message = "Something went wrong updating User" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            return new JsonResult(userToUpdate) { StatusCode = StatusCodes.Status200OK };
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
            if (!_service.IsUserExists(userId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.DeleteUser(userId).Result)
            {
                return new JsonResult(new { message = "Something went wrong deleting User" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new JsonResult(null) { StatusCode = StatusCodes.Status204NoContent };
        }
    }
}
