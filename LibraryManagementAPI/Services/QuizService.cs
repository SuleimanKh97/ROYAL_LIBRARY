using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly LibraryDbContext _context;

        public QuizService(LibraryDbContext context)
        {
            _context = context;
        }

        // Quiz Management
        public async Task<QuizListDto> GetQuizzesAsync(QuizFilterDto filter)
        {
            var query = _context.Quizzes
                .Include(q => q.Creator)
                .Include(q => q.Questions)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Subject))
                query = query.Where(q => q.Subject == filter.Subject);

            if (!string.IsNullOrEmpty(filter.Grade))
                query = query.Where(q => q.Grade == filter.Grade);

            if (filter.IsActive.HasValue)
                query = query.Where(q => q.IsActive == filter.IsActive.Value);

            if (filter.IsPublic.HasValue)
                query = query.Where(q => q.IsPublic == filter.IsPublic.Value);

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.ToLower();
                query = query.Where(q => 
                    q.Title.ToLower().Contains(searchTerm) ||
                    q.TitleArabic.ToLower().Contains(searchTerm) ||
                    q.Subject.ToLower().Contains(searchTerm) ||
                    q.Grade.ToLower().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize);

            var quizzes = await query
                .OrderByDescending(q => q.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(q => new QuizDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    TitleArabic = q.TitleArabic,
                    Description = q.Description,
                    DescriptionArabic = q.DescriptionArabic,
                    Subject = q.Subject,
                    Grade = q.Grade,
                    Chapter = q.Chapter,
                    TimeLimit = q.TimeLimit,
                    TotalQuestions = q.TotalQuestions,
                    PassingScore = q.PassingScore,
                    IsActive = q.IsActive,
                    IsPublic = q.IsPublic,
                    CreatedAt = q.CreatedAt,
                    CreatedBy = q.CreatedBy,
                    CreatorName = q.Creator != null ? $"{q.Creator.FirstName} {q.Creator.LastName}" : null
                })
                .ToListAsync();

            return new QuizListDto
            {
                Quizzes = quizzes,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<QuizDto?> GetQuizByIdAsync(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Creator)
                .Include(q => q.Questions.OrderBy(qq => qq.OrderIndex))
                .ThenInclude(q => q.Options.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
                return null;

            return new QuizDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                TitleArabic = quiz.TitleArabic,
                Description = quiz.Description,
                DescriptionArabic = quiz.DescriptionArabic,
                Subject = quiz.Subject,
                Grade = quiz.Grade,
                Chapter = quiz.Chapter,
                TimeLimit = quiz.TimeLimit,
                TotalQuestions = quiz.TotalQuestions,
                PassingScore = quiz.PassingScore,
                IsActive = quiz.IsActive,
                IsPublic = quiz.IsPublic,
                CreatedAt = quiz.CreatedAt,
                CreatedBy = quiz.CreatedBy,
                CreatorName = quiz.Creator != null ? $"{quiz.Creator.FirstName} {quiz.Creator.LastName}" : null,
                Questions = quiz.Questions.Select(q => new QuizQuestionDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    QuestionTextArabic = q.QuestionTextArabic,
                    Explanation = q.Explanation,
                    ExplanationArabic = q.ExplanationArabic,
                    Points = q.Points,
                    OrderIndex = q.OrderIndex,
                    Type = q.Type,
                    Options = q.Options.Select(o => new QuestionOptionDto
                    {
                        Id = o.Id,
                        OptionText = o.OptionText,
                        OptionTextArabic = o.OptionTextArabic,
                        IsCorrect = o.IsCorrect,
                        OrderIndex = o.OrderIndex
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto, string userId)
        {
            var quiz = new Quiz
            {
                Title = createQuizDto.Title,
                TitleArabic = createQuizDto.TitleArabic,
                Description = createQuizDto.Description,
                DescriptionArabic = createQuizDto.DescriptionArabic,
                Subject = createQuizDto.Subject,
                Grade = createQuizDto.Grade,
                Chapter = createQuizDto.Chapter,
                TimeLimit = createQuizDto.TimeLimit,
                PassingScore = createQuizDto.PassingScore,
                IsActive = createQuizDto.IsActive,
                IsPublic = createQuizDto.IsPublic,
                CreatedBy = userId,
                TotalQuestions = createQuizDto.Questions.Count
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            // Add questions
            foreach (var questionDto in createQuizDto.Questions)
            {
                var question = new QuizQuestion
                {
                    QuizId = quiz.Id,
                    QuestionText = questionDto.QuestionText,
                    QuestionTextArabic = questionDto.QuestionTextArabic,
                    Explanation = questionDto.Explanation,
                    ExplanationArabic = questionDto.ExplanationArabic,
                    Points = questionDto.Points,
                    OrderIndex = questionDto.OrderIndex,
                    Type = questionDto.Type
                };

                _context.QuizQuestions.Add(question);
                await _context.SaveChangesAsync();

                // Add options
                foreach (var optionDto in questionDto.Options)
                {
                    var option = new QuestionOption
                    {
                        QuestionId = question.Id,
                        OptionText = optionDto.OptionText,
                        OptionTextArabic = optionDto.OptionTextArabic,
                        IsCorrect = optionDto.IsCorrect,
                        OrderIndex = optionDto.OrderIndex
                    };

                    _context.QuestionOptions.Add(option);
                }
            }

            await _context.SaveChangesAsync();

            return await GetQuizByIdAsync(quiz.Id) ?? new QuizDto();
        }

        public async Task<QuizDto> UpdateQuizAsync(int id, UpdateQuizDto updateQuizDto)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                throw new InvalidOperationException("Quiz not found");

            quiz.Title = updateQuizDto.Title;
            quiz.TitleArabic = updateQuizDto.TitleArabic;
            quiz.Description = updateQuizDto.Description;
            quiz.DescriptionArabic = updateQuizDto.DescriptionArabic;
            quiz.Subject = updateQuizDto.Subject;
            quiz.Grade = updateQuizDto.Grade;
            quiz.Chapter = updateQuizDto.Chapter;
            quiz.TimeLimit = updateQuizDto.TimeLimit;
            quiz.PassingScore = updateQuizDto.PassingScore;
            quiz.IsActive = updateQuizDto.IsActive;
            quiz.IsPublic = updateQuizDto.IsPublic;
            quiz.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetQuizByIdAsync(id) ?? new QuizDto();
        }

        public async Task<bool> DeleteQuizAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return false;

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleQuizStatusAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return false;

            quiz.IsActive = !quiz.IsActive;
            quiz.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        // Question Management
        public async Task<QuizQuestionDto> AddQuestionToQuizAsync(int quizId, CreateQuizQuestionDto createQuestionDto)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null)
                throw new InvalidOperationException("Quiz not found");

            var question = new QuizQuestion
            {
                QuizId = quizId,
                QuestionText = createQuestionDto.QuestionText,
                QuestionTextArabic = createQuestionDto.QuestionTextArabic,
                Explanation = createQuestionDto.Explanation,
                ExplanationArabic = createQuestionDto.ExplanationArabic,
                Points = createQuestionDto.Points,
                OrderIndex = createQuestionDto.OrderIndex,
                Type = createQuestionDto.Type
            };

            _context.QuizQuestions.Add(question);
            await _context.SaveChangesAsync();

            // Add options
            foreach (var optionDto in createQuestionDto.Options)
            {
                var option = new QuestionOption
                {
                    QuestionId = question.Id,
                    OptionText = optionDto.OptionText,
                    OptionTextArabic = optionDto.OptionTextArabic,
                    IsCorrect = optionDto.IsCorrect,
                    OrderIndex = optionDto.OrderIndex
                };

                _context.QuestionOptions.Add(option);
            }

            await _context.SaveChangesAsync();

            // Update quiz total questions
            quiz.TotalQuestions = await _context.QuizQuestions.CountAsync(q => q.QuizId == quizId);
            await _context.SaveChangesAsync();

            return new QuizQuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                QuestionTextArabic = question.QuestionTextArabic,
                Explanation = question.Explanation,
                ExplanationArabic = question.ExplanationArabic,
                Points = question.Points,
                OrderIndex = question.OrderIndex,
                Type = question.Type,
                Options = createQuestionDto.Options.Select(o => new QuestionOptionDto
                {
                    OptionText = o.OptionText,
                    OptionTextArabic = o.OptionTextArabic,
                    IsCorrect = o.IsCorrect,
                    OrderIndex = o.OrderIndex
                }).ToList()
            };
        }

        public async Task<QuizQuestionDto> UpdateQuestionAsync(int questionId, UpdateQuizQuestionDto updateQuestionDto)
        {
            var question = await _context.QuizQuestions.FindAsync(questionId);
            if (question == null)
                throw new InvalidOperationException("Question not found");

            question.QuestionText = updateQuestionDto.QuestionText;
            question.QuestionTextArabic = updateQuestionDto.QuestionTextArabic;
            question.Explanation = updateQuestionDto.Explanation;
            question.ExplanationArabic = updateQuestionDto.ExplanationArabic;
            question.Points = updateQuestionDto.Points;
            question.OrderIndex = updateQuestionDto.OrderIndex;
            question.Type = updateQuestionDto.Type;
            question.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new QuizQuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                QuestionTextArabic = question.QuestionTextArabic,
                Explanation = question.Explanation,
                ExplanationArabic = question.ExplanationArabic,
                Points = question.Points,
                OrderIndex = question.OrderIndex,
                Type = question.Type
            };
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            var question = await _context.QuizQuestions.FindAsync(questionId);
            if (question == null)
                return false;

            _context.QuizQuestions.Remove(question);
            await _context.SaveChangesAsync();

            // Update quiz total questions
            var quiz = await _context.Quizzes.FindAsync(question.QuizId);
            if (quiz != null)
            {
                quiz.TotalQuestions = await _context.QuizQuestions.CountAsync(q => q.QuizId == question.QuizId);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> ReorderQuestionsAsync(int quizId, List<int> questionIds)
        {
            for (int i = 0; i < questionIds.Count; i++)
            {
                var question = await _context.QuizQuestions.FindAsync(questionIds[i]);
                if (question != null && question.QuizId == quizId)
                {
                    question.OrderIndex = i;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // Option Management
        public async Task<QuestionOptionDto> AddOptionToQuestionAsync(int questionId, CreateQuestionOptionDto createOptionDto)
        {
            var question = await _context.QuizQuestions.FindAsync(questionId);
            if (question == null)
                throw new InvalidOperationException("Question not found");

            var option = new QuestionOption
            {
                QuestionId = questionId,
                OptionText = createOptionDto.OptionText,
                OptionTextArabic = createOptionDto.OptionTextArabic,
                IsCorrect = createOptionDto.IsCorrect,
                OrderIndex = createOptionDto.OrderIndex
            };

            _context.QuestionOptions.Add(option);
            await _context.SaveChangesAsync();

            return new QuestionOptionDto
            {
                Id = option.Id,
                OptionText = option.OptionText,
                OptionTextArabic = option.OptionTextArabic,
                IsCorrect = option.IsCorrect,
                OrderIndex = option.OrderIndex
            };
        }

        public async Task<QuestionOptionDto> UpdateOptionAsync(int optionId, UpdateQuestionOptionDto updateOptionDto)
        {
            var option = await _context.QuestionOptions.FindAsync(optionId);
            if (option == null)
                throw new InvalidOperationException("Option not found");

            option.OptionText = updateOptionDto.OptionText;
            option.OptionTextArabic = updateOptionDto.OptionTextArabic;
            option.IsCorrect = updateOptionDto.IsCorrect;
            option.OrderIndex = updateOptionDto.OrderIndex;
            option.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new QuestionOptionDto
            {
                Id = option.Id,
                OptionText = option.OptionText,
                OptionTextArabic = option.OptionTextArabic,
                IsCorrect = option.IsCorrect,
                OrderIndex = option.OrderIndex
            };
        }

        public async Task<bool> DeleteOptionAsync(int optionId)
        {
            var option = await _context.QuestionOptions.FindAsync(optionId);
            if (option == null)
                return false;

            _context.QuestionOptions.Remove(option);
            await _context.SaveChangesAsync();
            return true;
        }

        // Quiz Attempts
        public async Task<QuizAttemptDto> StartQuizAttemptAsync(int quizId, string userId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions.OrderBy(qq => qq.OrderIndex))
                .ThenInclude(q => q.Options.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
                throw new InvalidOperationException("Quiz not found");

            if (!quiz.IsActive)
                throw new InvalidOperationException("Quiz is not active");

            var attempt = new QuizAttempt
            {
                QuizId = quizId,
                UserId = userId,
                TotalScore = quiz.Questions.Sum(q => q.Points),
                Status = AttemptStatus.InProgress
            };

            _context.QuizAttempts.Add(attempt);
            await _context.SaveChangesAsync();

            return new QuizAttemptDto
            {
                Id = attempt.Id,
                QuizId = attempt.QuizId,
                QuizTitle = quiz.Title,
                QuizTitleArabic = quiz.TitleArabic,
                UserId = attempt.UserId,
                StartedAt = attempt.StartedAt,
                TotalScore = attempt.TotalScore,
                Status = attempt.Status
            };
        }

        public async Task<QuizAttemptDto> SubmitQuizAttemptAsync(SubmitQuizAttemptDto submitAttemptDto, string userId)
        {
            var attempt = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .FirstOrDefaultAsync(a => a.Id == submitAttemptDto.AttemptId && a.UserId == userId);

            if (attempt == null)
                throw new InvalidOperationException("Attempt not found");

            if (attempt.Status != AttemptStatus.InProgress)
                throw new InvalidOperationException("Attempt is already completed");

            var questions = await _context.QuizQuestions
                .Include(q => q.Options)
                .Where(q => q.QuizId == attempt.QuizId)
                .ToListAsync();

            int totalScore = 0;
            var answers = new List<AttemptAnswer>();

            foreach (var answerDto in submitAttemptDto.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answerDto.QuestionId);
                if (question == null) continue;

                bool isCorrect = false;
                int pointsEarned = 0;

                switch (question.Type)
                {
                    case QuestionType.MultipleChoice:
                        var selectedOption = question.Options.FirstOrDefault(o => o.Id == answerDto.SelectedOptionId);
                        isCorrect = selectedOption?.IsCorrect ?? false;
                        break;
                    case QuestionType.TrueFalse:
                        var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                        isCorrect = correctOption != null && 
                                  bool.TryParse(correctOption.OptionText, out bool correctValue) &&
                                  answerDto.BooleanAnswer == correctValue;
                        break;
                    case QuestionType.FillInTheBlank:
                        var correctTextOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                        isCorrect = correctTextOption != null && 
                                  answerDto.TextAnswer?.Trim().ToLower() == correctTextOption.OptionText?.Trim().ToLower();
                        break;
                }

                if (isCorrect)
                {
                    pointsEarned = question.Points;
                    totalScore += question.Points;
                }

                var answer = new AttemptAnswer
                {
                    AttemptId = attempt.Id,
                    QuestionId = question.Id,
                    SelectedOptionId = answerDto.SelectedOptionId,
                    TextAnswer = answerDto.TextAnswer,
                    BooleanAnswer = answerDto.BooleanAnswer,
                    IsCorrect = isCorrect,
                    PointsEarned = pointsEarned
                };

                answers.Add(answer);
            }

            _context.AttemptAnswers.AddRange(answers);

            attempt.Score = totalScore;
            attempt.Percentage = attempt.TotalScore > 0 ? (double)totalScore / attempt.TotalScore * 100 : 0;
            attempt.IsPassed = attempt.Percentage >= attempt.Quiz.PassingScore;
            attempt.TimeSpent = submitAttemptDto.TimeSpent;
            attempt.CompletedAt = DateTime.UtcNow;
            attempt.Status = AttemptStatus.Completed;
            attempt.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAttemptByIdAsync(attempt.Id, userId) ?? new QuizAttemptDto();
        }

        public async Task<QuizAttemptDto?> GetAttemptByIdAsync(int attemptId, string userId)
        {
            var attempt = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .Include(a => a.User)
                .Include(a => a.Answers)
                .ThenInclude(aa => aa.Question)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(a => a.Id == attemptId && a.UserId == userId);

            if (attempt == null)
                return null;

            return new QuizAttemptDto
            {
                Id = attempt.Id,
                QuizId = attempt.QuizId,
                QuizTitle = attempt.Quiz.Title,
                QuizTitleArabic = attempt.Quiz.TitleArabic,
                UserId = attempt.UserId,
                UserName = attempt.User != null ? $"{attempt.User.FirstName} {attempt.User.LastName}" : "",
                StartedAt = attempt.StartedAt,
                CompletedAt = attempt.CompletedAt,
                Score = attempt.Score,
                TotalScore = attempt.TotalScore,
                Percentage = attempt.Percentage,
                IsPassed = attempt.IsPassed,
                TimeSpent = attempt.TimeSpent,
                Status = attempt.Status,
                Answers = attempt.Answers.Select(aa => new AttemptAnswerDto
                {
                    Id = aa.Id,
                    QuestionId = aa.QuestionId,
                    QuestionText = aa.Question.QuestionText,
                    QuestionTextArabic = aa.Question.QuestionTextArabic,
                    SelectedOptionId = aa.SelectedOptionId,
                    TextAnswer = aa.TextAnswer,
                    BooleanAnswer = aa.BooleanAnswer,
                    IsCorrect = aa.IsCorrect,
                    PointsEarned = aa.PointsEarned,
                    AnsweredAt = aa.AnsweredAt,
                    Options = aa.Question.Options.Select(o => new QuestionOptionDto
                    {
                        Id = o.Id,
                        OptionText = o.OptionText,
                        OptionTextArabic = o.OptionTextArabic,
                        IsCorrect = o.IsCorrect,
                        OrderIndex = o.OrderIndex
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<List<QuizAttemptDto>> GetUserAttemptsAsync(string userId)
        {
            var attempts = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.StartedAt)
                .Select(a => new QuizAttemptDto
                {
                    Id = a.Id,
                    QuizId = a.QuizId,
                    QuizTitle = a.Quiz.Title,
                    QuizTitleArabic = a.Quiz.TitleArabic,
                    UserId = a.UserId,
                    StartedAt = a.StartedAt,
                    CompletedAt = a.CompletedAt,
                    Score = a.Score,
                    TotalScore = a.TotalScore,
                    Percentage = a.Percentage,
                    IsPassed = a.IsPassed,
                    TimeSpent = a.TimeSpent,
                    Status = a.Status
                })
                .ToListAsync();

            return attempts;
        }

        public async Task<List<QuizAttemptDto>> GetQuizAttemptsAsync(int quizId)
        {
            var attempts = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .Include(a => a.User)
                .Where(a => a.QuizId == quizId)
                .OrderByDescending(a => a.StartedAt)
                .Select(a => new QuizAttemptDto
                {
                    Id = a.Id,
                    QuizId = a.QuizId,
                    QuizTitle = a.Quiz.Title,
                    QuizTitleArabic = a.Quiz.TitleArabic,
                    UserId = a.UserId,
                    UserName = a.User != null ? $"{a.User.FirstName} {a.User.LastName}" : "",
                    StartedAt = a.StartedAt,
                    CompletedAt = a.CompletedAt,
                    Score = a.Score,
                    TotalScore = a.TotalScore,
                    Percentage = a.Percentage,
                    IsPassed = a.IsPassed,
                    TimeSpent = a.TimeSpent,
                    Status = a.Status
                })
                .ToListAsync();

            return attempts;
        }

        // Statistics
        public async Task<QuizStatisticsDto> GetQuizStatisticsAsync()
        {
            var totalQuizzes = await _context.Quizzes.CountAsync();
            var activeQuizzes = await _context.Quizzes.CountAsync(q => q.IsActive);
            var totalAttempts = await _context.QuizAttempts.CountAsync();
            var completedAttempts = await _context.QuizAttempts.CountAsync(a => a.Status == AttemptStatus.Completed);
            
            var averageScore = completedAttempts > 0 
                ? await _context.QuizAttempts
                    .Where(a => a.Status == AttemptStatus.Completed)
                    .AverageAsync(a => a.Percentage)
                : 0;

            var passRate = completedAttempts > 0
                ? (double)await _context.QuizAttempts
                    .CountAsync(a => a.Status == AttemptStatus.Completed && a.IsPassed) / completedAttempts * 100
                : 0;

            var subjectStats = await GetSubjectStatisticsAsync();
            var gradeStats = await GetGradeStatisticsAsync();

            return new QuizStatisticsDto
            {
                TotalQuizzes = totalQuizzes,
                ActiveQuizzes = activeQuizzes,
                TotalAttempts = totalAttempts,
                CompletedAttempts = completedAttempts,
                AverageScore = averageScore,
                PassRate = passRate,
                SubjectStats = subjectStats,
                GradeStats = gradeStats
            };
        }

        public async Task<List<SubjectStatsDto>> GetSubjectStatisticsAsync()
        {
            var stats = await _context.Quizzes
                .GroupBy(q => q.Subject)
                .Select(g => new SubjectStatsDto
                {
                    Subject = g.Key,
                    QuizCount = g.Count(),
                    AttemptCount = g.SelectMany(q => q.Attempts).Count(),
                    AverageScore = g.SelectMany(q => q.Attempts)
                        .Where(a => a.Status == AttemptStatus.Completed)
                        .Any() 
                        ? g.SelectMany(q => q.Attempts)
                            .Where(a => a.Status == AttemptStatus.Completed)
                            .Average(a => a.Percentage)
                        : 0
                })
                .ToListAsync();

            return stats;
        }

        public async Task<List<GradeStatsDto>> GetGradeStatisticsAsync()
        {
            var stats = await _context.Quizzes
                .GroupBy(q => q.Grade)
                .Select(g => new GradeStatsDto
                {
                    Grade = g.Key,
                    QuizCount = g.Count(),
                    AttemptCount = g.SelectMany(q => q.Attempts).Count(),
                    AverageScore = g.SelectMany(q => q.Attempts)
                        .Where(a => a.Status == AttemptStatus.Completed)
                        .Any() 
                        ? g.SelectMany(q => q.Attempts)
                            .Where(a => a.Status == AttemptStatus.Completed)
                            .Average(a => a.Percentage)
                        : 0
                })
                .ToListAsync();

            return stats;
        }

        // Utility Methods
        public async Task<List<string>> GetAvailableSubjectsAsync()
        {
            return await _context.Quizzes
                .Where(q => q.IsActive)
                .Select(q => q.Subject)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
        }

        public async Task<List<string>> GetAvailableGradesAsync()
        {
            return await _context.Quizzes
                .Where(q => q.IsActive)
                .Select(q => q.Grade)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
        }

        public async Task<bool> IsQuizAccessibleAsync(int quizId, string userId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null || !quiz.IsActive)
                return false;

            if (quiz.IsPublic)
                return true;

            // For private quizzes, check if user is admin or creator
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            // Check if user is admin (you might need to implement role checking)
            // For now, we'll allow access if user is the creator
            return quiz.CreatedBy == userId;
        }
    }
} 