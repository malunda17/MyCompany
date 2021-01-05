using MediatR;
using System;

namespace MyCompany.ClaimService.Application.Commands
{
    public class UpdateClaimCommand :IRequest<bool>
    {
        public int ClaimId { get; set; }
        public DateTime Date { get; private set; }
        public int UserId { get; private set; }
        public string Description { get; private set; }

        public string Incidence { get; private set; }
        public string DamagedItem { get; private set; }
        public string Status { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }

        public string Country { get; private set; }

        public UpdateClaimCommand(int claimId,DateTime date, int userId, string description, string incidence, string status ,string damagedItem, string street, string city, string country)
        {
            ClaimId = claimId;
            Date = date;
            UserId = userId;
            Description = description;
            Incidence = incidence;
            DamagedItem = damagedItem;
            Status = status;
            Street = street;
            City = city;
            Country = country;
        }

    }
}