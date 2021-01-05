using Moq;
using MyCompany.LogService.Application.Models;
using MyCompany.LogService.Application.Queries;
using MyCompany.LogService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.LogService.UnitTests.Application.Queries
{
    public class GetAllLogsQueryHandlerTest
    {
        private readonly Mock<ILogRepository> _logRepositoryMock;
        public GetAllLogsQueryHandlerTest()
        {
            _logRepositoryMock = new Mock<ILogRepository>();
        }

        [Fact]
        public async Task Handler_GetAllLogsQueryObjectPassed_ProperMethodsCalledAndReturnAllLogs()
        {
            _logRepositoryMock.Setup(logRepository=> logRepository.GetAllAsync(It.IsAny<Expression<Func<Log, bool>>>())).Returns(Task.FromResult(GetLogs()));
            //Act
            var handler = new GetAllLogsQueryHandler(_logRepositoryMock.Object);
            var result = await handler.Handle(new GetAllLogsQuery(),new CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<LogViewModel>>(result);
            Assert.True(result.Count() == GetLogs().Count());
            _logRepositoryMock.Verify();
        }

        private IEnumerable<Log> GetLogs()
        {
            return new List<Log>
            {
                new Log(1,"user1","CreateClaim",DateTime.UtcNow),
                new Log(2,"user2","DelteClaim",DateTime.UtcNow),
                new Log(3,"user3","CreateUser",DateTime.UtcNow)
            };
        }
    }
}
