using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyCompany.LogService.Api.Controllers;
using MyCompany.LogService.Application.Commands;
using MyCompany.LogService.Application.Models;
using MyCompany.LogService.Application.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.LogService.UnitTests.Api.Controllers
{
    public class LogsControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public LogsControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Delete_LogIdPassed_ProperMethodsCalledAndReturn200HttpStatusCode()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<DeleteLogCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();
            //Act
            var logsController = new LogsController(_mediatorMock.Object);
            var result = await logsController.Delete(1);

            Assert.IsAssignableFrom<OkResult>(result);
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Delete_UnexistedLogIdPassed_ProperMethodsCalledAndReturn400HttpStatusCode()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<DeleteLogCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false).Verifiable();
            //Act
            var logsController = new LogsController(_mediatorMock.Object);
            var result = await logsController.Delete(1);
            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Get_ExistedLogIdPassed_ProperMethodsCalledAndReturnMatchedLog()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetLogQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new LogViewModel()).Verifiable();
            //Act
            var logsController = new LogsController(_mediatorMock.Object);
            var result = await logsController.Get(1);
            //Assert

            Assert.NotNull(result.Value);
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Get_UnexistedLogIdPassed_ProperMethodsCalledAndReturn404NotFoundResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetLogQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((LogViewModel)null).Verifiable();
            //Act
            var logsController = new LogsController(_mediatorMock.Object);
            var result = await logsController.Get(1);
            //Assert
            Assert.True(((StatusCodeResult)result.Result).StatusCode == (int)HttpStatusCode.NotFound);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Get_ProperMethodsCalledAndReturnListOfLogs()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllLogsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(GetLogs()).Verifiable();
            //Act
            var logsController = new LogsController(_mediatorMock.Object);
            var result = await logsController.Get();
            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == GetLogs().Count());
            _mediatorMock.Verify();
        }

        private IEnumerable<LogViewModel> GetLogs()
        {
            return new List<LogViewModel>
            {
                new LogViewModel(),
                new LogViewModel()
            };
        }


    }
}