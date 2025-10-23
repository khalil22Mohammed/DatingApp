using System.ComponentModel.DataAnnotations;

namespace FirstApp.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(4)]
        public string Password { get; set; } = "";
    }
}
