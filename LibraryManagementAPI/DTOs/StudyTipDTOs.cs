using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class StudyTipDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public string? Grade { get; set; }
        public string? Subject { get; set; }
        public List<string> Tips { get; set; } = new List<string>();
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateStudyTipDto
    {
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; }

        [StringLength(50)]
        public string? Subject { get; set; }

        public List<string> Tips { get; set; } = new List<string>();

        public int OrderIndex { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }

    public class UpdateStudyTipDto
    {
        [StringLength(100)]
        public string? Category { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; }

        [StringLength(50)]
        public string? Subject { get; set; }

        public List<string>? Tips { get; set; }

        public int? OrderIndex { get; set; }

        public bool? IsActive { get; set; }
    }

    public class StudyTipFilterDto
    {
        public string? Category { get; set; }
        public string? Grade { get; set; }
        public string? Subject { get; set; }
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
