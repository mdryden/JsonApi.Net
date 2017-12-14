using mdryden.JsonApi;
using System;
using System.Net;
using Xunit;

namespace mdryden.JsonApi.xUnit
{
    public class JsonApiExceptionTests
    {
        [Fact]
        public void CodeIsSetTest()
        {
            var expected = HttpStatusCode.OK;
            var target = new JsonApiException(expected);
            var actual = target.Code;

            Assert.Equal(expected, actual);
        }
    }
}

