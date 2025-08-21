using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class StudyMaterialDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string? Teacher { get; set; }
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Delivery { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Downloads { get; set; }
        public string Duration { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateStudyMaterialDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Teacher { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "متوفر";

        [StringLength(100)]
        public string Delivery { get; set; } = "PDF / طباعة";

        [StringLength(50)]
        public string Duration { get; set; } = string.Empty;

        public List<string> Features { get; set; } = new List<string>();

        public bool IsActive { get; set; } = true;
    }

    public class UpdateStudyMaterialDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [StringLength(50)]
        public string? Subject { get; set; }

        [StringLength(100)]
        public string? Teacher { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(100)]
        public string? Delivery { get; set; }

        [StringLength(50)]
        public string? Duration { get; set; }

        public List<string>? Features { get; set; }

        public bool? IsActive { get; set; }
    }

    public class StudyMaterialFilterDto
    {
        public string? Category { get; set; }
        public string? Subject { get; set; }
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
