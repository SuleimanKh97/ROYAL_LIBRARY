using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Models
{
    public class StudyMaterial
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // summaries, past-questions, worksheets

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty; // arabic, english, islamic, history

        [StringLength(100)]
        public string? Teacher { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = 0.0m;

        [StringLength(50)]
        public string Status { get; set; } = "متوفر"; // متوفر, غير متوفر

        [StringLength(100)]
        public string Delivery { get; set; } = "PDF / طباعة";

        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; } = 0.0m;

        public int Downloads { get; set; } = 0;

        [StringLength(50)]
        public string Duration { get; set; } = string.Empty; // 45 صفحة, 120 سؤال, 30 ورقة

        public string? Features { get; set; } // JSON array of features

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
