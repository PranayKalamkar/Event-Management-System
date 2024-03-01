using System.ComponentModel.DataAnnotations;

namespace Event_Management_App.Models
{
    public class Admin_UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Username must contain only letters.")]
        public string? Username { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must meet complexity requirements.")]
        public string? SPassword { get; set; }

        [Required]
        [Compare(nameof(SPassword), ErrorMessage = "Passwords do not match")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must meet complexity requirements.")]
        public string? ConfirmSPassword { get; set; }

        [Required]
        public int Role { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Contact must contain only numbers.")]
        public string? Contact { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,'.-]+$", ErrorMessage = "Address must contain letters, spaces, numbers, and other characters.")]
        public string? Address { get; set; }

        [Required]
        public string? IdProofPath { get; set; }

        public IFormFile? IdProofFile { get; set; }

        [Required]
        public string? ProfilePath { get; set; }

        public IFormFile? ProfileFile { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

		public int DeletedBy { get; set; }

		public DateTime DeletedAt { get; set; }

	}
}
