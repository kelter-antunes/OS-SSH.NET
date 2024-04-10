using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Tests.Common;
using Renci.OS.SshNet.Tests.Properties;

namespace Renci.OS.SshNet.Tests.Classes.Common
{
    /// <summary>
    /// Provides data for <see cref="Renci.OS.SshNet.ForwardedPort.RequestReceived"/> event.
    /// </summary>
    [TestClass]
    public class PortForwardEventArgsTest : TestBase
    {
        [TestMethod]
        public void ConstructorShouldThrowArgumentNullExceptionWhenHostIsNull()
        {
            try
            {
                _ = new PortForwardEventArgs(null, 80);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsNull(ex.InnerException);
                Assert.AreEqual("host", ex.ParamName);
            }
        }

        [TestMethod]
        public void ConstructorShouldNotThrowExceptionWhenHostIsEmpty()
        {
            var host = string.Empty;

            var eventArgs = new PortForwardEventArgs(host, 80);

            Assert.AreSame(host, eventArgs.OriginatorHost);
        }

        [TestMethod]
        public void ConstructorShouldNotThrowExceptionWhenHostIsInvalidDnsName()
        {
            const string host = "in_valid_host.";

            var eventArgs = new PortForwardEventArgs(host, 80);

            Assert.AreSame(host, eventArgs.OriginatorHost);
        }

        [TestMethod]
        public void ConstructorShouldThrowArgumentOutOfRangeExceptionWhenPortIsGreaterThanMaximumValue()
        {
            const int port = IPEndPoint.MaxPort + 1;

            try
            {
                _ = new PortForwardEventArgs(Resources.HOST, port);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsNull(ex.InnerException);
                Assert.AreEqual("port", ex.ParamName);
            }
        }
    }
}
