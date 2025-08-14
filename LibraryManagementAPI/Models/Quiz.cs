using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty; // الرياضيات، الفيزياء، الكيمياء، إلخ

        [Required]
        [StringLength(50)]
        public string Grade { get; set; } = string.Empty; // الصف الأول، الثاني، التوجيهي، إلخ

        [StringLength(100)]
        public string? Chapter { get; set; } // الفصل الدراسي أو الوحدة

        public int TimeLimit { get; set; } = 30; // الوقت بالدقائق

        public int TotalQuestions { get; set; } = 0;

        public int PassingScore { get; set; } = 60; // النسبة المئوية للنجاح

        public bool IsActive { get; set; } = true;

        public bool IsPublic { get; set; } = true; // هل الكويز متاح للجميع أم للمسجلين فقط

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; } // معرف المستخدم الذي أنشأ الكويز

        // Navigation properties
        public virtual ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public virtual ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
        public virtual User? Creator { get; set; }
    }
} 