using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameArabic { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DescriptionArabic { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int BookCount { get; set; }
    }

    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NameArabic { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Icon { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NameArabic { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? DescriptionArabic { get; set; }

        [StringLength(50)]
        public string? Icon { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

