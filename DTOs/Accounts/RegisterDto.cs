using System.ComponentModel.DataAnnotations;

namespace Tour_API.DTOs.Accounts
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email {  get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
