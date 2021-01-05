using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.IdentityService.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
    }
}