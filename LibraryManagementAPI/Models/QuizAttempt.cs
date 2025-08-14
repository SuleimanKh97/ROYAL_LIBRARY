using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public int Score { get; set; } = 0; // النقاط المحققة

        public int TotalScore { get; set; } = 0; // إجمالي النقاط الممكنة

        public double Percentage { get; set; } = 0; // النسبة المئوية

        public bool IsPassed { get; set; } = false;

        public int TimeSpent { get; set; } = 0; // الوقت المستغرق بالثواني

        public AttemptStatus Status { get; set; } = AttemptStatus.InProgress;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AttemptAnswer> Answers { get; set; } = new List<AttemptAnswer>();
    }

    public enum AttemptStatus
    {
        InProgress,  // قيد التنفيذ
        Completed,   // مكتمل
        Abandoned,   // متروك
        TimeExpired  // انتهى الوقت
    }
} 