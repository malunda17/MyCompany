using System;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.AgentApplication.Dtos
{
    public class ClaimDTO
    {
        public int ClaimId { get;  set; }
        
        public int UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Incidence { get; set; }
        [Required]
        public string DamagedItem { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

    }
}