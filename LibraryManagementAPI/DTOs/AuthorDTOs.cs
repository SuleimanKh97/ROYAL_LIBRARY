using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameArabic { get; set; }
        public string? Biography { get; set; }
        public string? BiographyArabic { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int BookCount { get; set; }
    }

    public class CreateAuthorDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? NameArabic { get; set; }

        public string? Biography { get; set; }

        public string? BiographyArabic { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string? Nationality { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateAuthorDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? NameArabic { get; set; }

        public string? Biography { get; set; }

        public string? BiographyArabic { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string? Nationality { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

