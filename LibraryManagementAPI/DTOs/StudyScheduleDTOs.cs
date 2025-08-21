using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class StudyScheduleDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? TitleArabic { get; set; }
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public string? Grade { get; set; }
        public List<string> Subjects { get; set; } = new List<string>();
        public string? Focus { get; set; }
        public string? FocusArabic { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateStudyScheduleDto
    {
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Day { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; }

        public List<string> Subjects { get; set; } = new List<string>();

        [StringLength(200)]
        public string? Focus { get; set; }

        [StringLength(200)]
        public string? FocusArabic { get; set; }

        public int OrderIndex { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }

    public class UpdateStudyScheduleDto
    {
        [StringLength(50)]
        public string? Type { get; set; }

        [StringLength(50)]
        public string? Day { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; }

        public List<string>? Subjects { get; set; }

        [StringLength(200)]
        public string? Focus { get; set; }

        [StringLength(200)]
        public string? FocusArabic { get; set; }

        public int? OrderIndex { get; set; }

        public bool? IsActive { get; set; }
    }

    public class StudyScheduleFilterDto
    {
        public string? Type { get; set; }
        public string? Grade { get; set; }
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
