using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorNameArabic { get; set; }
        public int? PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherNameArabic { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryNameArabic { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? Pages { get; set; }
        public string Language { get; set; } = "Arabic";
        public decimal? Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNewRelease { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BookImageDto> Images { get; set; } = new List<BookImageDto>();
    }

    public class CreateBookDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        [StringLength(20)]
        public string? ISBN { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        public int? AuthorId { get; set; }

        public int? PublisherId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int? Pages { get; set; }

        [StringLength(20)]
        public string Language { get; set; } = "Arabic";

        public decimal? Price { get; set; }

        public decimal? OriginalPrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        [StringLength(500)]
        public string? CoverImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public bool IsNewRelease { get; set; } = false;
    }

    public class UpdateBookDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        [StringLength(20)]
        public string? ISBN { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        public int? AuthorId { get; set; }

        public int? PublisherId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int? Pages { get; set; }

        [StringLength(20)]
        public string Language { get; set; } = "Arabic";

        public decimal? Price { get; set; }

        public decimal? OriginalPrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        [StringLength(500)]
        public string? CoverImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public bool IsNewRelease { get; set; } = false;
    }

    public class BookSearchDto
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public int? PublisherId { get; set; }
        public string? Language { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsNewRelease { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SortBy { get; set; } = "Title";
        public string SortOrder { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class BookImageDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageType { get; set; } = "Gallery";
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateBookImageDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public string ImageType { get; set; } = "Gallery";

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}

