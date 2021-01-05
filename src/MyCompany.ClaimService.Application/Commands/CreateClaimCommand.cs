using MediatR;
using System;
using System.Runtime.Serialization;

namespace MyCompany.ClaimService.Application.Commands
{
    [DataContract]
    public class CreateClaimCommand : IRequest<int>
    {
        [DataMember]
        public DateTime Date { get; private set; }
        public int UserId { get; private set; }
        public string Description { get; private set; }

        public string Incidence { get; private set; }
        public string DamagedItem { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }

        public string Country { get; private set; }

        public CreateClaimCommand(DateTime date, int userId, string description, string incidence, string damagedItem, string street, string city, string country)
        {
            Date = date;
            UserId = userId;
            Description = description;
            Incidence = incidence;
            DamagedItem = damagedItem;

            Street = street;
            City = city;
            Country = country;
        }
    }
}