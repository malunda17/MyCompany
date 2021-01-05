using System;

namespace MyCompany.ClaimService.Application.Queries
{

    public class ClaimDTO
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Incidence { get; set; }
        public string DamagedItem { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}