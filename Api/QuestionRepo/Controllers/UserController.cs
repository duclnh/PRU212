using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.UserBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;

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
        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(UserGet))]
        public async Task<JsonResult> GetUser(string username)
        {
            var isExists = await _service.IsUserExists(username);
            if (!isExists)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var User = _mapper.Map<UserGet>(await _service.GetUser(username));
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
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var userGet = _mapper.Map<User>(await _service.GetUser(user.Username));
            if (user.Password != userGet.Password)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(new { message = "Successfully login!" }) { StatusCode = StatusCodes.Status200OK };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> PutUser(int userId, [FromBody] UserDto userToUpdate)
        {
            if (userToUpdate == null)
            {
                return new JsonResult(new { message = "User is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (userId != userToUpdate.UserId)
            {
                return new JsonResult(new { message = "User id mismatch" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.IsUserExists(userId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            var userMap = _mapper.Map<User>(userToUpdate);
            if (!_service.UpdateUser(userMap).Result)
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
        public async Task<JsonResult> PostUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return new JsonResult(new { message = "User is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var users = await _service.GetUsers();
            var user = users.FirstOrDefault(q => q.Username.Trim().ToUpper() == userCreate.Username.Trim().ToUpper());
            if (user != null)
            {
                return new JsonResult(new { message = "Username already exists." }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToList());

                return new JsonResult(new { message = "Model validation failed.", errors = errors }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var userMap = _mapper.Map<User>(userCreate);
            if (!_service.AddUser(userMap).Result)
            {
                return new JsonResult(new { message = "Failed to add User." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new JsonResult(new { message = "Successfully created!" }) { StatusCode = StatusCodes.Status200OK };
        }

        // DELETE: api/Users/5
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> DeleteUser(int userId)
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
