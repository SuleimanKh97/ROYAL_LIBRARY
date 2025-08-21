using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudySchedulesController : ControllerBase
    {
        private readonly IStudyScheduleService _studyScheduleService;

        public StudySchedulesController(IStudyScheduleService studyScheduleService)
        {
            _studyScheduleService = studyScheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyScheduleDto>>> GetAll([FromQuery] StudyScheduleFilterDto filter)
        {
            try
            {
                var schedules = await _studyScheduleService.GetAllAsync(filter);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyScheduleDto>> GetById(int id)
        {
            try
            {
                var schedule = await _studyScheduleService.GetByIdAsync(id);
                if (schedule == null)
                    return NotFound(new { message = "Study schedule not found" });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudyScheduleDto>> Create(CreateStudyScheduleDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var schedule = await _studyScheduleService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = schedule.Id }, schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudyScheduleDto>> Update(int id, UpdateStudyScheduleDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var schedule = await _studyScheduleService.UpdateAsync(id, updateDto);
                return Ok(schedule);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _studyScheduleService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Study schedule not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            try
            {
                var types = await _studyScheduleService.GetTypesAsync();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("grades")]
        public async Task<ActionResult<IEnumerable<string>>> GetGrades()
        {
            try
            {
                var grades = await _studyScheduleService.GetGradesAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
