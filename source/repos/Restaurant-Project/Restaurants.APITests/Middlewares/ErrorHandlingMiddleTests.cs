using Xunit;
using Restaurants.API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Restaurants.API.Middlewares.Tests
{
    public class ErrorHandlingMiddleTests
    {
        [Fact()]
        public async Task InvokeAsyncTest_whenNoExceptionThrown_ShouclCallNextDelegate()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
            var middleware = new ErrorHandlingMiddle(loggerMock.Object);

            var context = new DefaultHttpContext();

            var nextDelegateMock = new Mock<RequestDelegate>();

            //act

            await middleware.InvokeAsync(context, nextDelegateMock.Object);

            //Assert

            nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);


        }
    }
}