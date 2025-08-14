using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementAPI.Services;
using LibraryManagementAPI.DTOs;
using System.Security.Claims;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/quizzes
        [HttpGet]
        public async Task<ActionResult<QuizListDto>> GetQuizzes([FromQuery] QuizFilterDto filter)
        {
            try
            {
                var quizzes = await _quizService.GetQuizzesAsync(filter);
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب الكويزات", error = ex.Message });
            }
        }

        // GET: api/quizzes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuiz(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdAsync(id);
                if (quiz == null)
                    return NotFound(new { message = "الكويز غير موجود" });

                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب الكويز", error = ex.Message });
            }
        }

        // POST: api/quizzes
        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "يجب تسجيل الدخول" });

                var quiz = await _quizService.CreateQuizAsync(createQuizDto, userId);
                return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إنشاء الكويز", error = ex.Message });
            }
        }

        // PUT: api/quizzes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuizDto>> UpdateQuiz(int id, [FromBody] UpdateQuizDto updateQuizDto)
        {
            try
            {
                var quiz = await _quizService.UpdateQuizAsync(id, updateQuizDto);
                return Ok(quiz);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء تحديث الكويز", error = ex.Message });
            }
        }

        // DELETE: api/quizzes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            try
            {
                var result = await _quizService.DeleteQuizAsync(id);
                if (!result)
                    return NotFound(new { message = "الكويز غير موجود" });

                return Ok(new { message = "تم حذف الكويز بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء حذف الكويز", error = ex.Message });
            }
        }

        // PATCH: api/quizzes/{id}/toggle
        [HttpPatch("{id}/toggle")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> ToggleQuizStatus(int id)
        {
            try
            {
                var result = await _quizService.ToggleQuizStatusAsync(id);
                if (!result)
                    return NotFound(new { message = "الكويز غير موجود" });

                return Ok(new { message = "تم تغيير حالة الكويز بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء تغيير حالة الكويز", error = ex.Message });
            }
        }

        // POST: api/quizzes/{quizId}/questions
        [HttpPost("{quizId}/questions")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuizQuestionDto>> AddQuestion(int quizId, [FromBody] CreateQuizQuestionDto createQuestionDto)
        {
            try
            {
                var question = await _quizService.AddQuestionToQuizAsync(quizId, createQuestionDto);
                return CreatedAtAction(nameof(GetQuiz), new { id = quizId }, question);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إضافة السؤال", error = ex.Message });
            }
        }

        // PUT: api/quizzes/questions/{questionId}
        [HttpPut("questions/{questionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuizQuestionDto>> UpdateQuestion(int questionId, [FromBody] UpdateQuizQuestionDto updateQuestionDto)
        {
            try
            {
                var question = await _quizService.UpdateQuestionAsync(questionId, updateQuestionDto);
                return Ok(question);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء تحديث السؤال", error = ex.Message });
            }
        }

        // DELETE: api/quizzes/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var result = await _quizService.DeleteQuestionAsync(questionId);
                if (!result)
                    return NotFound(new { message = "السؤال غير موجود" });

                return Ok(new { message = "تم حذف السؤال بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء حذف السؤال", error = ex.Message });
            }
        }

        // POST: api/quizzes/questions/{questionId}/options
        [HttpPost("questions/{questionId}/options")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuestionOptionDto>> AddOption(int questionId, [FromBody] CreateQuestionOptionDto createOptionDto)
        {
            try
            {
                var option = await _quizService.AddOptionToQuestionAsync(questionId, createOptionDto);
                return Ok(option);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إضافة الخيار", error = ex.Message });
            }
        }

        // PUT: api/quizzes/options/{optionId}
        [HttpPut("options/{optionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuestionOptionDto>> UpdateOption(int optionId, [FromBody] UpdateQuestionOptionDto updateOptionDto)
        {
            try
            {
                var option = await _quizService.UpdateOptionAsync(optionId, updateOptionDto);
                return Ok(option);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء تحديث الخيار", error = ex.Message });
            }
        }

        // DELETE: api/quizzes/options/{optionId}
        [HttpDelete("options/{optionId}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> DeleteOption(int optionId)
        {
            try
            {
                var result = await _quizService.DeleteOptionAsync(optionId);
                if (!result)
                    return NotFound(new { message = "الخيار غير موجود" });

                return Ok(new { message = "تم حذف الخيار بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء حذف الخيار", error = ex.Message });
            }
        }

        // POST: api/quizzes/{quizId}/start
        [HttpPost("{quizId}/start")]
        [Authorize]
        public async Task<ActionResult<QuizAttemptDto>> StartQuiz(int quizId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "يجب تسجيل الدخول" });

                var attempt = await _quizService.StartQuizAttemptAsync(quizId, userId);
                return Ok(attempt);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء بدء الكويز", error = ex.Message });
            }
        }

        // POST: api/quizzes/submit
        [HttpPost("submit")]
        [Authorize]
        public async Task<ActionResult<QuizAttemptDto>> SubmitQuiz([FromBody] SubmitQuizAttemptDto submitAttemptDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "يجب تسجيل الدخول" });

                var attempt = await _quizService.SubmitQuizAttemptAsync(submitAttemptDto, userId);
                return Ok(attempt);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إرسال الكويز", error = ex.Message });
            }
        }

        // GET: api/quizzes/attempts/{attemptId}
        [HttpGet("attempts/{attemptId}")]
        [Authorize]
        public async Task<ActionResult<QuizAttemptDto>> GetAttempt(int attemptId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "يجب تسجيل الدخول" });

                var attempt = await _quizService.GetAttemptByIdAsync(attemptId, userId);
                if (attempt == null)
                    return NotFound(new { message = "المحاولة غير موجودة" });

                return Ok(attempt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب المحاولة", error = ex.Message });
            }
        }

        // GET: api/quizzes/my-attempts
        [HttpGet("my-attempts")]
        [Authorize]
        public async Task<ActionResult<List<QuizAttemptDto>>> GetMyAttempts()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "يجب تسجيل الدخول" });

                var attempts = await _quizService.GetUserAttemptsAsync(userId);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب المحاولات", error = ex.Message });
            }
        }

        // GET: api/quizzes/{quizId}/attempts
        [HttpGet("{quizId}/attempts")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<List<QuizAttemptDto>>> GetQuizAttempts(int quizId)
        {
            try
            {
                var attempts = await _quizService.GetQuizAttemptsAsync(quizId);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب محاولات الكويز", error = ex.Message });
            }
        }

        // GET: api/quizzes/statistics
        [HttpGet("statistics")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult<QuizStatisticsDto>> GetStatistics()
        {
            try
            {
                var statistics = await _quizService.GetQuizStatisticsAsync();
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب الإحصائيات", error = ex.Message });
            }
        }

        // GET: api/quizzes/subjects
        [HttpGet("subjects")]
        public async Task<ActionResult<List<string>>> GetSubjects()
        {
            try
            {
                var subjects = await _quizService.GetAvailableSubjectsAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب المواد", error = ex.Message });
            }
        }

        // GET: api/quizzes/grades
        [HttpGet("grades")]
        public async Task<ActionResult<List<string>>> GetGrades()
        {
            try
            {
                var grades = await _quizService.GetAvailableGradesAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب الصفوف", error = ex.Message });
            }
        }
    }
} 