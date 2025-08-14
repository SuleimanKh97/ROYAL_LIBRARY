using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Services;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _environment;

        public BooksController(IBookService bookService, IWebHostEnvironment environment)
        {
            _bookService = bookService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookSearchDto searchDto)
        {
            var result = await _bookService.GetBooksAsync(searchDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            // Increment view count
            await _bookService.IncrementViewCountAsync(id);

            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                        .ToList();
                    
                    return BadRequest(new { 
                        message = "Validation failed", 
                        errors = errors 
                    });
                }

                var book = await _bookService.CreateBookAsync(createBookDto);
                if (book == null)
                    return BadRequest(new { message = "Failed to create book. Please check your data and try again." });

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { 
                    message = "An error occurred while creating the book", 
                    details = ex.Message 
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookService.UpdateBookAsync(id, updateBookDto);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Book deleted successfully" });
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedBooks([FromQuery] int count = 10)
        {
            var books = await _bookService.GetFeaturedBooksAsync(count);
            return Ok(books);
        }

        [HttpGet("new-releases")]
        public async Task<IActionResult> GetNewReleases([FromQuery] int count = 10)
        {
            var books = await _bookService.GetNewReleasesAsync(count);
            return Ok(books);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId, [FromQuery] int count = 10)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryId, count);
            return Ok(books);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId, [FromQuery] int count = 10)
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId, count);
            return Ok(books);
        }

        [HttpGet("publisher/{publisherId}")]
        public async Task<IActionResult> GetBooksByPublisher(int publisherId, [FromQuery] int count = 10)
        {
            var books = await _bookService.GetBooksByPublisherAsync(publisherId, count);
            return Ok(books);
        }

        [HttpPatch("{id}/stock")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UpdateBookStock(int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var result = await _bookService.UpdateBookStockAsync(id, updateStockDto.Quantity);
            if (!result)
                return NotFound();

            return Ok(new { message = "Stock updated successfully" });
        }

        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetBookImages(int id)
        {
            var images = await _bookService.GetBookImagesAsync(id);
            return Ok(images);
        }

        [HttpPost("images")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> AddBookImage([FromBody] CreateBookImageDto createImageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = await _bookService.AddBookImageAsync(createImageDto);
            if (image == null)
                return BadRequest(new { message = "Failed to add book image" });

            return Ok(image);
        }

        [HttpPost("upload-image")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "No file was uploaded" });
                }

                // Validate file type
                var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
                if (!allowedTypes.Contains(file.ContentType.ToLower()))
                {
                    return BadRequest(new { message = "Invalid file type. Only JPG, PNG, GIF, and WebP images are allowed." });
                }

                // Validate file size (5MB)
                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest(new { message = "File size too large. Maximum size is 5MB." });
                }

                // Ensure uploads directory exists
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "books");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the public URL
                var request = HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                var imageUrl = $"{baseUrl}/uploads/books/{fileName}";
                
                return Ok(new { 
                    message = "Image uploaded successfully", 
                    imageUrl = imageUrl,
                    fileName = fileName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { 
                    message = "An error occurred while uploading the image", 
                    details = ex.Message 
                });
            }
        }

        [HttpDelete("images/{imageId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteBookImage(int imageId)
        {
            var result = await _bookService.DeleteBookImageAsync(imageId);
            if (!result)
                return NotFound();

            return Ok(new { message = "Book image deleted successfully" });
        }
    }

    public class UpdateStockDto
    {
        public int Quantity { get; set; }
    }
}

