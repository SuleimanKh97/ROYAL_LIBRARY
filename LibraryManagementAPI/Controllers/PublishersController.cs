using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherService.GetPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActivePublishers()
        {
            var publishers = await _publisherService.GetActivePublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPublishers([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest(new { message = "Search term is required" });

            var publishers = await _publisherService.SearchPublishersAsync(searchTerm);
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto createPublisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = await _publisherService.CreatePublisherAsync(createPublisherDto);
            if (publisher == null)
                return BadRequest(new { message = "Failed to create publisher" });

            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] UpdatePublisherDto updatePublisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = await _publisherService.UpdatePublisherAsync(id, updatePublisherDto);
            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherService.DeletePublisherAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Publisher deleted successfully" });
        }
    }
}

