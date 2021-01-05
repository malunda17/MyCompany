using Moq;
using MyCompany.ClaimService.Application.Commands;
using MyCompany.ClaimService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Application.Commands
{
    public class UpdateClaimCommandHandlerTest
    {
        private readonly Mock<IClaimRepository> _claimRepositoryMock;

        public UpdateClaimCommandHandlerTest()
        {
            _claimRepositoryMock = new Mock<IClaimRepository>();
        }

        [Fact]
        public async Task Handler_UpdateClaimCommandObjectPassed_ProperMethodsCalledAndReturnTrue()
        {
            _claimRepositoryMock.Setup(claimRepository => claimRepository.UpdateAsync(It.IsAny<Claim>())).ReturnsAsync(true).Verifiable();
            _claimRepositoryMock.Setup(claimRepository => claimRepository.GetAsync(It.IsAny<int>())).ReturnsAsync(GetClaim()).Verifiable();
            //Act
            var handler = new UpdateClaimCommandHandler(_claimRepositoryMock.Object);
            var result = await handler.Handle(new UpdateClaimCommand(1, DateTime.UtcNow, 2, "descripton", "incidence", "Review" ,"damagedItem", "street", "city", "country"), new CancellationToken());
            //Assert
            Assert.True(result);
            _claimRepositoryMock.Verify();
        }

        private Claim GetClaim()
        {
            return new Claim(DateTime.UtcNow, 1, "claim description", "incidence", "damagedItem", "street", "city", "country");
        }
    }
}