using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

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

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? OriginalPrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        [StringLength(500)]
        public string? CoverImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public bool IsNewRelease { get; set; } = false;

        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; } = 0.0m;

        public int RatingCount { get; set; } = 0;

        public int ViewCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Author? Author { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();
        public virtual ICollection<BookInquiry> BookInquiries { get; set; } = new List<BookInquiry>();
    }
}

