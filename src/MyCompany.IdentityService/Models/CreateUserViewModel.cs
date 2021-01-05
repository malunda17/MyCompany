using System.ComponentModel.DataAnnotations;

namespace MyCompany.IdentityService.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}