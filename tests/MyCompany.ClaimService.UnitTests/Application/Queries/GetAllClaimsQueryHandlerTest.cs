using Moq;
using MyCompany.ClaimService.Application.Queries;
using MyCompany.ClaimService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Application.Queries
{
    public class GetAllClaimsQueryHandlerTest
    {
        private readonly Mock<IClaimRepository> _claimRepositoryMock;
        public GetAllClaimsQueryHandlerTest()
        {
            _claimRepositoryMock = new Mock<IClaimRepository>();
        }
        [Fact]
        public async Task Handler_GetAllClaimsQueryObjectPassed_ProperMethodsCalledAndReturnListOfClaims()
        {
            _claimRepositoryMock.Setup(claimRepository => claimRepository.GetAllAsync(It.IsAny<Expression<Func<Claim, bool>>>())).ReturnsAsync(GetClaims()).Verifiable();
            //Act
            var handler = new GetAllClaimsQueryHandler(_claimRepositoryMock.Object);
            var result = await handler.Handle(new GetAllClaimsQuery(), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2,result.Count());
            _claimRepositoryMock.Verify();
        }

        private IEnumerable<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(DateTime.UtcNow, 1, "claim description", "incidence", "damagedItem", "street", "city", "country"),
                new Claim(DateTime.UtcNow, 3, "claim description", "incidence", "damagedItem", "street", "city", "country")
            };
        }

    }
}
