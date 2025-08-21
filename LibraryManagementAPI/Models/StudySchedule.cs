using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Models
{
    public class StudySchedule
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // daily, weekly

        [Required]
        [StringLength(50)]
        public string Day { get; set; } = string.Empty; // الأحد, الاثنين, etc. or 06:00-07:00 for daily

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; } // first-year, second-year, all

        public string? Subjects { get; set; } // JSON array of subjects

        [StringLength(200)]
        public string? Focus { get; set; }

        [StringLength(200)]
        public string? FocusArabic { get; set; }

        public int OrderIndex { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
