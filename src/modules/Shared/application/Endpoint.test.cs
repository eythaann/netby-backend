using Microsoft.AspNetCore.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shared.Application.Tests
{
    [TestClass]
    public class BaseEndpointTests
    {
        private class MockEndpoint : BaseEndpoint
        {
            public MockEndpoint(string? path) : base(path) { }

            public override void Register(WebApplication app)
            {
                // Mock implementation for testing
            }
        }

        [TestMethod]
        public void BaseEndpoint_InitializationWithNullPath_ShouldSetRootToEmptyString()
        {
            string? path = null;
            var endpoint = new MockEndpoint(path);
            Assert.AreEqual(string.Empty, endpoint.root);
        }

        [TestMethod]
        public void BaseEndpoint_InitializationWithPath_ShouldSetRootToPath()
        {
            string path = "/api";
            var endpoint = new MockEndpoint(path);
            Assert.AreEqual(path, endpoint.root);
        }
    }
}
