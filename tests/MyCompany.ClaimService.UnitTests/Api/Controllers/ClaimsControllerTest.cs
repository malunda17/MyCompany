using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyCompany.ClaimService.Api.Controllers;
using MyCompany.ClaimService.Application.Commands;
using MyCompany.ClaimService.Application.Queries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCompany.ClaimService.UnitTests.Api.Controllers
{
    public  class ClaimsControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        public ClaimsControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Create_CreateClaimCommandObjectPassed_ProperMethodsCalledAndReturnCreateResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateClaimCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Create(new CreateClaimCommand(DateTime.UtcNow, 2, "descripton", "incidence", "damagedItem", "street", "city", "country"));
            //Assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Update_UpdateExistedClaim_ProperMethodsCalledAndReturnOkResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<UpdateClaimCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result =  await controller.Update(new UpdateClaimCommand(1, DateTime.UtcNow, 2, "descripton", "incidence", "Review", "damagedItem", "street", "city", "country"));
            //Assert
            Assert.True(((StatusCodeResult)result).StatusCode == (int)HttpStatusCode.OK);
            _mediatorMock.Verify();

        }

        [Fact]
        public async Task Update_UpdateUnexistedClaim_ProperMethodsCalledAndReturnNotFoundResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<UpdateClaimCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Update(new UpdateClaimCommand(1, DateTime.UtcNow, 2, "descripton", "incidence", "Review", "damagedItem", "street", "city", "country"));
            //Assert
            Assert.True(((StatusCodeResult)result).StatusCode == (int)HttpStatusCode.NotFound);
            _mediatorMock.Verify();

        }


        [Fact]
        public async Task Delete_ExistedClaimdIdPassed_ProperMethodsCalledAndReturnOkResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<DeleteClaimCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Delete(1);
            //Assert
            Assert.IsAssignableFrom<OkResult>(result);
            _mediatorMock.Verify();

        }
        [Fact]
        public async Task Delete_UnexistedClaimdIdPassed_ProperMethodsCalledAndReturNotFoundResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<DeleteClaimCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Delete(1);
            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
            _mediatorMock.Verify();

        }

        [Fact]
        public async Task Get_ExistedClaimIdPassed_ProperMethodsCalledAndReturnMatchedClaim()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetClaimQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ClaimDTO()).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Get(1);
            //Assert
            Assert.NotNull(result.Value);
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Get_UnexistedClaimIdPassed_ProperMethodsCalledAndReturnNotFoundResult()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetClaimQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((ClaimDTO)null).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Get(1);
            //Assert
            Assert.True(((StatusCodeResult)result.Result).StatusCode == (int)HttpStatusCode.NotFound);
            _mediatorMock.Verify();
        }

        [Fact]
        public async Task Get_ProperMethodsCalledAndReturnListOfClaims()
        {
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllClaimsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<ClaimDTO> { new ClaimDTO(),new ClaimDTO()}).Verifiable();
            //Act
            var controller = new ClaimsController(_mediatorMock.Object);
            var result = await controller.Get();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

    }
}