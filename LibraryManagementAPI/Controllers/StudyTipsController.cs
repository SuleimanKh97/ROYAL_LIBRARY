using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyTipsController : ControllerBase
    {
        private readonly IStudyTipService _studyTipService;

        public StudyTipsController(IStudyTipService studyTipService)
        {
            _studyTipService = studyTipService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyTipDto>>> GetAll([FromQuery] StudyTipFilterDto filter)
        {
            try
            {
                var tips = await _studyTipService.GetAllAsync(filter);
                return Ok(tips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyTipDto>> GetById(int id)
        {
            try
            {
                var tip = await _studyTipService.GetByIdAsync(id);
                if (tip == null)
                    return NotFound(new { message = "Study tip not found" });

                return Ok(tip);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudyTipDto>> Create(CreateStudyTipDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tip = await _studyTipService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = tip.Id }, tip);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudyTipDto>> Update(int id, UpdateStudyTipDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tip = await _studyTipService.UpdateAsync(id, updateDto);
                return Ok(tip);
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
                var result = await _studyTipService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Study tip not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            try
            {
                var categories = await _studyTipService.GetCategoriesAsync();
                return Ok(categories);
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
                var grades = await _studyTipService.GetGradesAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("subjects")]
        public async Task<ActionResult<IEnumerable<string>>> GetSubjects()
        {
            try
            {
                var subjects = await _studyTipService.GetSubjectsAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
