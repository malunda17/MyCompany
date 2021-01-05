using MediatR;
using MyCompany.ClaimService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Commands
{
    public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, bool>
    {
        private readonly IClaimRepository _claimRepository;

        public UpdateClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<bool> Handle(UpdateClaimCommand command, CancellationToken cancellationToken)
        {
            var claimToUpdate = await _claimRepository.GetAsync(command.ClaimId);
            if (claimToUpdate == null)
            {
                return false;
            }

            claimToUpdate.UserId = command.UserId;
            claimToUpdate.Description = command.Description;
            claimToUpdate.DamagedItem = command.DamagedItem;
            claimToUpdate.Incidence = command.Incidence;
            claimToUpdate.Status = (ClaimStatus)Enum.Parse(typeof(ClaimStatus), command.Status);
            claimToUpdate.Date = command.Date;
            claimToUpdate.Street = command.Street;
            claimToUpdate.City = command.City;
            claimToUpdate.Country = command.Country;

            return await _claimRepository.UpdateAsync(claimToUpdate); ;
        }
    }
}