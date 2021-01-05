using Moq;
using MyCompany.ClaimService.Application.Commands;
using MyCompany.ClaimService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Application.Commands
{
    public class CreateClaimCommandHandlerTest
    {
        private readonly Mock<IClaimRepository> _claimRepositoryMock;
        public CreateClaimCommandHandlerTest()
        {
            _claimRepositoryMock = new Mock<IClaimRepository>();
        }
        [Fact]
        public async Task Handler_CreateClaimCommandObjectPassed_ProperMethodsCalledAndReturnCreatedClaimId()
        {
            _claimRepositoryMock.Setup(claimRepository => claimRepository.AddAsync(It.IsAny<Claim>())).ReturnsAsync(true).Verifiable();
            //Act
            var handler = new CreateClaimCommandHandler(_claimRepositoryMock.Object);
            var result = await handler.Handle(GetCreateCommand(),new CancellationToken());
            //Asset
            _claimRepositoryMock.Verify();
        }

        private CreateClaimCommand GetCreateCommand()
        {
            return new CreateClaimCommand(DateTime.UtcNow,1,"claim description","incidence","damagedItem","street","city","country");
        }
    }
}