using Moq;
using MyCompany.LogService.Application.Queries;
using MyCompany.LogService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.LogService.UnitTests.Application.Queries
{
    public class GetLogQueryHandlerTest
    {
        private readonly Mock<ILogRepository> _logRepositoryMock;

        public GetLogQueryHandlerTest()
        {
            _logRepositoryMock = new Mock<ILogRepository>();
        }

        [Fact]
        public async Task Handler_GetLogQueryPassed_ProperMethodsCalledAndReturnMatchedLog()
        {
            _logRepositoryMock.Setup(logRepository => logRepository.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Log(1, "username", "action",DateTime.UtcNow))).Verifiable();
            //Act
            var handler = new GetLogQueryHandler(_logRepositoryMock.Object);
            var result = await handler.Handle(new GetLogQuery(1), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            _logRepositoryMock.Verify();
        }
    }
}