using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.Services
{
    public interface IQuizService
    {
        // Quiz Management
        Task<QuizListDto> GetQuizzesAsync(QuizFilterDto filter);
        Task<QuizDto?> GetQuizByIdAsync(int id);
        Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto, string userId);
        Task<QuizDto> UpdateQuizAsync(int id, UpdateQuizDto updateQuizDto);
        Task<bool> DeleteQuizAsync(int id);
        Task<bool> ToggleQuizStatusAsync(int id);

        // Question Management
        Task<QuizQuestionDto> AddQuestionToQuizAsync(int quizId, CreateQuizQuestionDto createQuestionDto);
        Task<QuizQuestionDto> UpdateQuestionAsync(int questionId, UpdateQuizQuestionDto updateQuestionDto);
        Task<bool> DeleteQuestionAsync(int questionId);
        Task<bool> ReorderQuestionsAsync(int quizId, List<int> questionIds);

        // Option Management
        Task<QuestionOptionDto> AddOptionToQuestionAsync(int questionId, CreateQuestionOptionDto createOptionDto);
        Task<QuestionOptionDto> UpdateOptionAsync(int optionId, UpdateQuestionOptionDto updateOptionDto);
        Task<bool> DeleteOptionAsync(int optionId);

        // Quiz Attempts
        Task<QuizAttemptDto> StartQuizAttemptAsync(int quizId, string userId);
        Task<QuizAttemptDto> SubmitQuizAttemptAsync(SubmitQuizAttemptDto submitAttemptDto, string userId);
        Task<QuizAttemptDto?> GetAttemptByIdAsync(int attemptId, string userId);
        Task<List<QuizAttemptDto>> GetUserAttemptsAsync(string userId);
        Task<List<QuizAttemptDto>> GetQuizAttemptsAsync(int quizId);

        // Statistics
        Task<QuizStatisticsDto> GetQuizStatisticsAsync();
        Task<List<SubjectStatsDto>> GetSubjectStatisticsAsync();
        Task<List<GradeStatsDto>> GetGradeStatisticsAsync();

        // Utility Methods
        Task<List<string>> GetAvailableSubjectsAsync();
        Task<List<string>> GetAvailableGradesAsync();
        Task<bool> IsQuizAccessibleAsync(int quizId, string userId);
    }
} 