using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



        // Navigation properties
        public virtual ICollection<BookInquiry> BookInquiries { get; set; } = new List<BookInquiry>();
        public virtual ICollection<Quiz> CreatedQuizzes { get; set; } = new List<Quiz>();
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }

    public enum UserRole
    {
        Admin,
        Librarian,
        Customer
    }
}

