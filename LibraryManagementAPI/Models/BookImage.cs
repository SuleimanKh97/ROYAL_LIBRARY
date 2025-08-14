using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class BookImage
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public ImageType ImageType { get; set; } = ImageType.Gallery;

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
    }

    public enum ImageType
    {
        Cover,
        Gallery,
        Thumbnail
    }
}

