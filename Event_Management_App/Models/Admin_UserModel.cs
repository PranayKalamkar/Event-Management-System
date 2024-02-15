using System.ComponentModel.DataAnnotations;

namespace Event_Management_App.Models
{
    public class Admin_UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters and numbers.")]
        [RegularExpression(@"^[a-zA-Z]+\\s[a-zA-Z]+$", ErrorMessage = "Username must contain only letters and numbers.")]
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

    }
}
