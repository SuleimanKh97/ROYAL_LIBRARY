using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAuthors()
        {
            var authors = await _authorService.GetActiveAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAuthors([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest(new { message = "Search term is required" });

            var authors = await _authorService.SearchAuthorsAsync(searchTerm);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = await _authorService.CreateAuthorAsync(createAuthorDto);
            if (author == null)
                return BadRequest(new { message = "Failed to create author" });

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = await _authorService.UpdateAuthorAsync(id, updateAuthorDto);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Author deleted successfully" });
        }
    }
}

