using Moq;
using MyCompany.ClaimService.Application.Commands;
using MyCompany.ClaimService.Domain;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Application.Commands
{
    public class DeleteClaimCommandHandlerTest
    {
        private readonly Mock<IClaimRepository> _claimRepositoryMock;

        public DeleteClaimCommandHandlerTest()
        {
            _claimRepositoryMock = new Mock<IClaimRepository>();
        }

        [Fact]
        public async Task Handler_DeleteClaimCommandObjectPassed_ProperMethodsCalledAndReturnTrue()
        {
            _claimRepositoryMock.Setup(claimRepository => claimRepository.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            //Act
            var handler = new DeleteClaimCommandHandler(_claimRepositoryMock.Object);
            var result = await handler.Handle(new DeleteClaimCommand(1), new CancellationToken());
            //Assert
            Assert.True(result);

            _claimRepositoryMock.Verify();
        }
    }
}