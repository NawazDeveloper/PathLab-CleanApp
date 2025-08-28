
using System.ComponentModel.DataAnnotations;

namespace ClarmindsApp.DTOs
{
    public class RegisterationRequest
    {
        [Required,MinLength(4)]
        public string FirstName { get; set; }
        [Required,MinLength(4)]

        public string LastName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        
        [Required, MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number and one special character")]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Passwords must match")]

        public string ConfirmPassword { get; set; }
        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string Education { get; set; }
    }
}
