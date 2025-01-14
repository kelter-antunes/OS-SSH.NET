﻿using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Renci.OS.SshNet.Tests.Classes
{
    public partial class SftpClientTest
    {
        [TestMethod]
        public async Task ConnectAsync_HostNameInvalid_ShouldThrowSocketExceptionWithErrorCodeHostNotFound()
        {
            var connectionInfo = new ConnectionInfo(Guid.NewGuid().ToString("N"), 40, "user",
                new KeyboardInteractiveAuthenticationMethod("user"));
            var sftpClient = new SftpClient(connectionInfo);

            try
            {
                await sftpClient.ConnectAsync(default);
                Assert.Fail();
            }
            catch (SocketException ex)
            {
                Assert.IsTrue(ex.SocketErrorCode is SocketError.HostNotFound or SocketError.TryAgain, $"Socket error is {ex.SocketErrorCode}");
            }
        }

        [TestMethod]
        public async Task ConnectAsync_ProxyHostNameInvalid_ShouldThrowSocketExceptionWithErrorCodeHostNotFound()
        {
            var connectionInfo = new ConnectionInfo("localhost", 40, "user", ProxyTypes.Http, Guid.NewGuid().ToString("N"), 80,
                "proxyUser", "proxyPwd", new KeyboardInteractiveAuthenticationMethod("user"));
            var sftpClient = new SftpClient(connectionInfo);

            try
            {
                await sftpClient.ConnectAsync(default);
                Assert.Fail();
            }
            catch (SocketException ex)
            {
                Assert.IsTrue(ex.SocketErrorCode is SocketError.HostNotFound or SocketError.TryAgain, $"Socket error is {ex.SocketErrorCode}");
            }
        }
    }
}
