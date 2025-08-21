using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Models
{
    public class StudyTip
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty; // إعداد البيئة الدراسية, تقنيات الدراسة الفعالة, إدارة الوقت, etc.

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Grade { get; set; } // first-year, second-year, all

        [StringLength(50)]
        public string? Subject { get; set; } // arabic, english, islamic, history, all

        public string? Tips { get; set; } // JSON array of tips

        public int OrderIndex { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
