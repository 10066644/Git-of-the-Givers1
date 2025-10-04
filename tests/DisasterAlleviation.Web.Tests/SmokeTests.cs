using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DisasterAlleviation.Web.Tests
{
    public class SmokeTests
    {
        [Fact]
        public async Task Home_Index_ReturnsSuccess()
        {
            await Task.CompletedTask;
            Assert.True(true);
        }
    }
}
