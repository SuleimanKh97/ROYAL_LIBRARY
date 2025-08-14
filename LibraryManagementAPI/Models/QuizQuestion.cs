using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        public string? QuestionTextArabic { get; set; }

        public string? Explanation { get; set; } // شرح الإجابة الصحيحة

        public string? ExplanationArabic { get; set; }

        public int Points { get; set; } = 1; // نقاط السؤال

        public int OrderIndex { get; set; } = 0; // ترتيب السؤال في الكويز

        public QuestionType Type { get; set; } = QuestionType.MultipleChoice;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }

    public enum QuestionType
    {
        MultipleChoice, // اختيار من متعدد
        TrueFalse,      // صح أو خطأ
        FillInTheBlank  // إكمال الفراغ
    }
} 