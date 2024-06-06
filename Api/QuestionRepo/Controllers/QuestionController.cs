using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.QuestionBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        // GET: api/Questions
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Question>))]
        public async Task<JsonResult> GetQuestions()
        {
            var questions = await _service.GetQuestions();
            if (questions == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(questions) { StatusCode = StatusCodes.Status200OK };
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(QuestionDto))]
        public async Task<JsonResult> RandomQuestion(Guid userId)
        {
            var question = await _service.RandomQuestion(userId);
            if (question == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(question) { StatusCode = StatusCodes.Status200OK };
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{questionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> PutQuestion(Guid questionId, [FromBody] QuestionDto questionToUpdate)
        {
            if (questionToUpdate == null)
            {
                return new JsonResult(new { message = "Question is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.IsQuestionExists(questionId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            var questions = _service.GetQuestions().Result;
            var isConflict = questions.Where(q => q.QuestionId != questionToUpdate.QuestionId).Any(q => q.Question1 == questionToUpdate.Question1 && q.QuestionId != questionId);
            if (isConflict)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status409Conflict };
            }


            var isUpdated = _service.UpdateQuestion(questionToUpdate).Result;
            if (!isUpdated)
            {
                return new JsonResult(new { message = "Something went wrong updating Question" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            return new JsonResult(questionToUpdate) { StatusCode = StatusCodes.Status200OK };
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<JsonResult> PostQuestion([FromBody] QuestionDto questionCreate)
        {
            if (questionCreate == null)
            {
                var errorResponse = new { message = "Question is required" };
                return new JsonResult(errorResponse) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var questions = await _service.GetQuestions();
            var question = questions.FirstOrDefault(q => q.Question1.Trim().ToUpper() == questionCreate.Question1.Trim().ToUpper());
            if (question != null)
            {
                var errorResponse = new { message = "Question already exists." };
                return new JsonResult(errorResponse) { StatusCode = StatusCodes.Status422UnprocessableEntity };
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToList());

                var errorResponse = new { message = "Model validation failed.", errors = errors };
                return new JsonResult(errorResponse) { StatusCode = StatusCodes.Status400BadRequest };
            }

            questionCreate.QuestionId = Guid.NewGuid();
            if (!_service.AddQuestion(questionCreate).Result)
            {
                var errorResponse = new { message = "Failed to add question." };
                return new JsonResult(errorResponse) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            var result = new JsonResult(new { message = "Successfully created!" }) { StatusCode = StatusCodes.Status200OK };
            return result;
        }

        // DELETE: api/Questions/5
        [HttpDelete("{questionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> DeleteQuestion(Guid questionId)
        {
            if (!_service.IsQuestionExists(questionId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.DeleteQuestion(questionId).Result)
            {
                ModelState.AddModelError("", "Something went wrong deleting question");
            }

            return new JsonResult(null) { StatusCode = StatusCodes.Status204NoContent };
        }
    }
}
