using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRepo.Business.RecordBusiness;
using QuestionRepo.Dto;
using QuestionRepo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _service;
        private readonly IMapper _mapper;

        public RecordController(IRecordService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Records
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RecordDto>))]
        public async Task<JsonResult> GetRecords()
        {
            var Records = await _service.GetRecords();
            if (Records == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var RecordsDto = _mapper.Map<IEnumerable<RecordDto>>(Records);
            return new JsonResult(RecordsDto) { StatusCode = StatusCodes.Status200OK };
        }

        // GET: api/Records/5
        [HttpGet("{recordId}")]
        [ProducesResponseType(200, Type = typeof(RecordDto))]
        public async Task<JsonResult> GetRecord(int recordId)
        {
            var isExists = await _service.IsRecordExists(recordId);
            if (!isExists)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }
            var Record = _mapper.Map<RecordDto>(await _service.GetRecord(recordId));
            return new JsonResult(Record) { StatusCode = StatusCodes.Status200OK };
        }

        // PUT: api/Records/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{recordId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> PutRecord(int recordId, [FromBody] RecordDto RecordToUpdate)
        {
            if (RecordToUpdate == null)
            {
                return new JsonResult(new { message = "Record is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (recordId != RecordToUpdate.RecordId)
            {
                return new JsonResult(new { message = "Record id mismatch" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.IsRecordExists(recordId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            var RecordMap = _mapper.Map<Record>(RecordToUpdate);
            if (!_service.UpdateRecord(RecordMap).Result)
            {
                return new JsonResult(new { message = "Something went wrong updating Record" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            return new JsonResult(RecordToUpdate) { StatusCode = StatusCodes.Status200OK };
        }

        // POST: api/Records
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<JsonResult> PostRecord([FromBody] RecordDto RecordCreate)
        {
            if (RecordCreate == null)
            {
                return new JsonResult(new { message = "Record is required" }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var Records = await _service.GetRecords();
            var Record = Records.FirstOrDefault(q => q.QuestionId == RecordCreate.QuestionId);
            if (Record != null)
            {
                return new JsonResult(new { message = "Record already exists." }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToList());

                return new JsonResult(new { message = "Model validation failed.", errors = errors }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var RecordMap = _mapper.Map<Record>(RecordCreate);
            if (!_service.AddRecord(RecordMap).Result)
            {
                return new JsonResult(new { message = "Failed to add Record." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            var result = new JsonResult(new { message = "Successfully created!" });
            result.StatusCode = StatusCodes.Status200OK;
            return result;
        }

        // DELETE: api/Records/5
        [HttpDelete("{recordId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> DeleteRecord(int recordId)
        {
            if (!_service.IsRecordExists(recordId).Result)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
            }

            if (!ModelState.IsValid)
            {
                return new JsonResult(ModelState) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (!_service.DeleteRecord(recordId).Result)
            {
                return new JsonResult(new { message = "Something went wrong deleting Record" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return new JsonResult(null) { StatusCode = StatusCodes.Status204NoContent };
        }
    }
}
