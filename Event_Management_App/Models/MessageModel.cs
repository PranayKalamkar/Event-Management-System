using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Event_Management_App.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Location must contain only letters.")]
        public string? Location { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Capacity must contain only numbers.")]
        public string? Capacity { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Budget must contain only numbers.")]
        public string? Budget { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Occassion must contain only letters.")]
        public string? Occassion { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s,'.-]+$", ErrorMessage = "Description must contain letters, spaces, numbers, and other characters.")]
        public string? Description { get; set; }

        [Required]
        [ForeignKey("signup")]
        public int Signup_Id { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
