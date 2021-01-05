using System;

namespace MyCompany.ClaimService.Domain
{
    public class Claim
    {
        public int ClaimId { get; private set; }
        public int UserId { get;  set; }
        public DateTime Date { get;  set; }
        public string Description { get;  set; }
        public ClaimStatus Status { get;  set; }
        public string Incidence { get;  set; }
        public string DamagedItem { get; set; }
        public string Street { get;  set; }
        public string City { get;  set; }
        public string Country { get;  set; }

        public Claim(DateTime date, int userId, string description, string incidence, string damagedItem , string street, string city, string country)
        {
            Date = date;
            UserId = userId;
            Description = description;
            Incidence = incidence;
            DamagedItem = damagedItem;
            Status = ClaimStatus.New;
            Street = street;
            City = city;
            Country = country;
        }

       
    }
}