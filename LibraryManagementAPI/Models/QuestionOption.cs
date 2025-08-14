using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string OptionText { get; set; } = string.Empty;

        public string? OptionTextArabic { get; set; }

        public bool IsCorrect { get; set; } = false;

        public int OrderIndex { get; set; } = 0; // ترتيب الخيار

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual QuizQuestion Question { get; set; } = null!;
    }
} 