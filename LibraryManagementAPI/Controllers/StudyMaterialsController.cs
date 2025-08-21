using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyMaterialsController : ControllerBase
    {
        private readonly IStudyMaterialService _studyMaterialService;

        public StudyMaterialsController(IStudyMaterialService studyMaterialService)
        {
            _studyMaterialService = studyMaterialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudyMaterialDto>>> GetAll([FromQuery] StudyMaterialFilterDto filter)
        {
            try
            {
                var materials = await _studyMaterialService.GetAllAsync(filter);
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyMaterialDto>> GetById(int id)
        {
            try
            {
                var material = await _studyMaterialService.GetByIdAsync(id);
                if (material == null)
                    return NotFound(new { message = "Study material not found" });

                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudyMaterialDto>> Create(CreateStudyMaterialDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var material = await _studyMaterialService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = material.Id }, material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudyMaterialDto>> Update(int id, UpdateStudyMaterialDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var material = await _studyMaterialService.UpdateAsync(id, updateDto);
                return Ok(material);
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
                var result = await _studyMaterialService.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = "Study material not found" });

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
                var categories = await _studyMaterialService.GetCategoriesAsync();
                return Ok(categories);
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
                var subjects = await _studyMaterialService.GetSubjectsAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
