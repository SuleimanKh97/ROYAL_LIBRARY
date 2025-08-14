using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class AttemptAnswer
    {
        public int Id { get; set; }

        [Required]
        public int AttemptId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public int? SelectedOptionId { get; set; } // للأسئلة متعددة الخيارات

        public string? TextAnswer { get; set; } // للإجابة النصية

        public bool? BooleanAnswer { get; set; } // لأسئلة صح أو خطأ

        public bool IsCorrect { get; set; } = false;

        public int PointsEarned { get; set; } = 0;

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual QuizAttempt Attempt { get; set; } = null!;
        public virtual QuizQuestion Question { get; set; } = null!;
        public virtual QuestionOption? SelectedOption { get; set; }
    }
} 