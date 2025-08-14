using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class BookInquiry
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [StringLength(100)]
        public string? CustomerName { get; set; }

        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? CustomerEmail { get; set; }

        public string? Message { get; set; }

        public InquiryStatus Status { get; set; } = InquiryStatus.Pending;

        public bool WhatsAppMessageSent { get; set; } = false;

        public string? ResponseMessage { get; set; }

        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual User? User { get; set; }
    }

    public enum InquiryStatus
    {
        Pending,
        Responded,
        Completed,
        Cancelled
    }
}

