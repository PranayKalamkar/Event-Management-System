using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Event_Management_App.Models
{
    public class AddEventModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category must contain only letters.")]
        public string Category { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Location must contain only letters.")]
        public string Location { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Capacity must contain only numbers.")]
        public string Capacity { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Amount must contain only numbers.")]
        public string Amount { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,'.-]+$", ErrorMessage = "Description must contain letters, spaces, numbers, and other characters.")]
        public string Description { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public IFormFile ImageFile { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,'.-]+$", ErrorMessage = "Address must contain letters, spaces, numbers, and other characters.")]
        public string Address { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Contact must contain only numbers.")]
        public string Contact { get; set; }

        [Required]
        [ForeignKey("eventstatus")]
        public int Status_Id { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
