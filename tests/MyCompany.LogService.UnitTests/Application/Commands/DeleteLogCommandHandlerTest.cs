using System.Threading.Tasks;
using Xunit;
using MyCompany.LogService.Domain;
using Moq;
using System.Threading;
using MyCompany.LogService.Application.Commands;

namespace MyCompany.LogService.UnitTests.Application.Commands
{
    public class DeleteLogCommandHandlerTest
    {
        private readonly Mock<ILogRepository> _logRepositoryMock;

        public DeleteLogCommandHandlerTest()
        {
            _logRepositoryMock = new Mock<ILogRepository>();
        }

        [Fact]
        public async Task Handler_DeleteLogCommandPassed_ProperMethodsCalledAndReturnTrue()
        {
            _logRepositoryMock.Setup(logRepository => logRepository.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            //Act
            var handler = new DeleteLogCommandHandler(_logRepositoryMock.Object);
            var result = await handler.Handle(new DeleteLogCommand(1), new CancellationToken());
            //Assert
            Assert.True(result);
            _logRepositoryMock.Verify(logRepository=> logRepository.DeleteAsync(It.IsAny<int>()));
        }
    }
}
