using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;
using System.Security.Claims;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookInquiriesController : ControllerBase
    {
        private readonly IBookInquiryService _bookInquiryService;

        public BookInquiriesController(IBookInquiryService bookInquiryService)
        {
            _bookInquiryService = bookInquiryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetBookInquiries([FromQuery] BookInquirySearchDto searchDto)
        {
            var result = await _bookInquiryService.GetBookInquiriesAsync(searchDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetBookInquiry(int id)
        {
            var inquiry = await _bookInquiryService.GetBookInquiryByIdAsync(id);
            if (inquiry == null)
                return NotFound();

            return Ok(inquiry);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookInquiry([FromBody] CreateBookInquiryDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var inquiry = await _bookInquiryService.CreateBookInquiryAsync(createDto, userId);
            
            if (inquiry == null)
                return BadRequest(new { message = "Book not found" });

            return CreatedAtAction(nameof(GetBookInquiry), new { id = inquiry.Id }, inquiry);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateBookInquiryStatus(int id, [FromBody] UpdateBookInquiryStatusDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookInquiryService.UpdateBookInquiryStatusAsync(id, updateDto);
            if (!result)
                return NotFound();

            return Ok(new { message = "Inquiry status updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBookInquiry(int id)
        {
            var result = await _bookInquiryService.DeleteBookInquiryAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Book inquiry deleted successfully" });
        }

        [HttpGet("{bookId}/whatsapp-url")]
        public async Task<IActionResult> GetWhatsAppUrl(int bookId, [FromQuery] string customerName, [FromQuery] string phoneNumber)
        {
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phoneNumber))
                return BadRequest(new { message = "Customer name and phone number are required" });

            var whatsappUrl = await _bookInquiryService.GetWhatsAppUrlAsync(bookId, customerName, phoneNumber);
            if (string.IsNullOrEmpty(whatsappUrl))
                return NotFound(new { message = "Book not found" });

            return Ok(new { whatsappUrl });
        }

        [HttpGet("{bookId}/whatsapp-message")]
        public async Task<IActionResult> GenerateWhatsAppMessage(int bookId, [FromQuery] string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
                return BadRequest(new { message = "Customer name is required" });

            var message = await _bookInquiryService.GenerateWhatsAppMessageAsync(bookId, customerName);
            if (string.IsNullOrEmpty(message))
                return NotFound(new { message = "Book not found" });

            return Ok(new { message });
        }
    }
}

