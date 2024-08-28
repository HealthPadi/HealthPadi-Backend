
using System.ComponentModel.DataAnnotations;

namespace HealthPadiWebApi.DTOs.Request
{
    public class RegisterRequestDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\\d]).{8,}", ErrorMessage = "Password must have at least 1 uppercase letter, 1 lowercase letter, 1 number, 1 non-alphanumeric character, and be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
