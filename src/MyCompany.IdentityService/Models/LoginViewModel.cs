using System.ComponentModel.DataAnnotations;

namespace MyCompany.IdentityService.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}