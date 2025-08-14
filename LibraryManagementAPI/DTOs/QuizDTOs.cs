using System.ComponentModel.DataAnnotations;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.DTOs
{
    // Quiz DTOs
    public class QuizDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleArabic { get; set; }
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string? Chapter { get; set; }
        public int TimeLimit { get; set; }
        public int TotalQuestions { get; set; }
        public int PassingScore { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatorName { get; set; }
        public List<QuizQuestionDto> Questions { get; set; } = new List<QuizQuestionDto>();
    }

    public class CreateQuizDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Grade { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Chapter { get; set; }

        public int TimeLimit { get; set; } = 30;
        public int PassingScore { get; set; } = 60;
        public bool IsActive { get; set; } = true;
        public bool IsPublic { get; set; } = true;
        public List<CreateQuizQuestionDto> Questions { get; set; } = new List<CreateQuizQuestionDto>();
    }

    public class UpdateQuizDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string? TitleArabic { get; set; }

        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Grade { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Chapter { get; set; }

        public int TimeLimit { get; set; }
        public int PassingScore { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
    }

    // Question DTOs
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionTextArabic { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationArabic { get; set; }
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        public QuestionType Type { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new List<QuestionOptionDto>();
    }

    public class CreateQuizQuestionDto
    {
        [Required]
        public string QuestionText { get; set; } = string.Empty;

        public string? QuestionTextArabic { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationArabic { get; set; }
        public int Points { get; set; } = 1;
        public int OrderIndex { get; set; } = 0;
        public QuestionType Type { get; set; } = QuestionType.MultipleChoice;
        public List<CreateQuestionOptionDto> Options { get; set; } = new List<CreateQuestionOptionDto>();
    }

    public class UpdateQuizQuestionDto
    {
        [Required]
        public string QuestionText { get; set; } = string.Empty;

        public string? QuestionTextArabic { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationArabic { get; set; }
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        public QuestionType Type { get; set; }
    }

    // Option DTOs
    public class QuestionOptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public string? OptionTextArabic { get; set; }
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateQuestionOptionDto
    {
        [Required]
        public string OptionText { get; set; } = string.Empty;

        public string? OptionTextArabic { get; set; }
        public bool IsCorrect { get; set; } = false;
        public int OrderIndex { get; set; } = 0;
    }

    public class UpdateQuestionOptionDto
    {
        [Required]
        public string OptionText { get; set; } = string.Empty;

        public string? OptionTextArabic { get; set; }
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    // Quiz Attempt DTOs
    public class QuizAttemptDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;
        public string? QuizTitleArabic { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool IsPassed { get; set; }
        public int TimeSpent { get; set; }
        public AttemptStatus Status { get; set; }
        public List<AttemptAnswerDto> Answers { get; set; } = new List<AttemptAnswerDto>();
    }

    public class StartQuizAttemptDto
    {
        [Required]
        public int QuizId { get; set; }
    }

    public class SubmitQuizAttemptDto
    {
        [Required]
        public int AttemptId { get; set; }
        public List<SubmitAnswerDto> Answers { get; set; } = new List<SubmitAnswerDto>();
        public int TimeSpent { get; set; }
    }

    // Answer DTOs
    public class AttemptAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionTextArabic { get; set; }
        public int? SelectedOptionId { get; set; }
        public string? SelectedOptionText { get; set; }
        public string? TextAnswer { get; set; }
        public bool? BooleanAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }
        public DateTime AnsweredAt { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new List<QuestionOptionDto>();
    }

    public class SubmitAnswerDto
    {
        [Required]
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public string? TextAnswer { get; set; }
        public bool? BooleanAnswer { get; set; }
    }

    // Quiz Filter DTOs
    public class QuizFilterDto
    {
        public string? Subject { get; set; }
        public string? Grade { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPublic { get; set; }
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class QuizListDto
    {
        public List<QuizDto> Quizzes { get; set; } = new List<QuizDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    // Statistics DTOs
    public class QuizStatisticsDto
    {
        public int TotalQuizzes { get; set; }
        public int ActiveQuizzes { get; set; }
        public int TotalAttempts { get; set; }
        public int CompletedAttempts { get; set; }
        public double AverageScore { get; set; }
        public double PassRate { get; set; }
        public List<SubjectStatsDto> SubjectStats { get; set; } = new List<SubjectStatsDto>();
        public List<GradeStatsDto> GradeStats { get; set; } = new List<GradeStatsDto>();
    }

    public class SubjectStatsDto
    {
        public string Subject { get; set; } = string.Empty;
        public int QuizCount { get; set; }
        public int AttemptCount { get; set; }
        public double AverageScore { get; set; }
    }

    public class GradeStatsDto
    {
        public string Grade { get; set; } = string.Empty;
        public int QuizCount { get; set; }
        public int AttemptCount { get; set; }
        public double AverageScore { get; set; }
    }


} 