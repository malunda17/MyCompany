using Moq;
using MyCompany.ClaimService.Application.Queries;
using MyCompany.ClaimService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Application.Queries
{
    public class GetClaimQueryHandlerTest
    {
        private readonly Mock<IClaimRepository> _claimRepositoryMock;
        public GetClaimQueryHandlerTest()
        {
            _claimRepositoryMock = new Mock<IClaimRepository>();
        }
        [Fact]
        public async Task Handler_GetClaimQueryObjectPassed_ProperMethodsCalledAndReturnMatchedClaim()
        {
            _claimRepositoryMock.Setup(claimRepository => claimRepository.GetAsync(It.IsAny<int>())).ReturnsAsync(GetClaim()).Verifiable();
            //Act
            var handler = new GetClaimQueryHandler(_claimRepositoryMock.Object);
            var result = await handler.Handle(new GetClaimQuery(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            _claimRepositoryMock.Verify();
        }

        private Claim GetClaim()
        {
            return new Claim(DateTime.UtcNow, 1, "claim description", "incidence", "damagedItem", "street", "city", "country");
        }
    }
}