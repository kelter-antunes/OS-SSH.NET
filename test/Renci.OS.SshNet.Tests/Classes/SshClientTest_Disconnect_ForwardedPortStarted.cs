﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Renci.OS.SshNet.Tests.Classes
{
    [TestClass]
    public class SshClientTest_Disconnect_ForwardedPortStarted : BaseClientTestBase
    {
        private Mock<ForwardedPort> _forwardedPortMock;
        private SshClient _sshClient;
        private ConnectionInfo _connectionInfo;

        protected override void CreateMocks()
        {
            base.CreateMocks();

            _forwardedPortMock = new Mock<ForwardedPort>(MockBehavior.Strict);
        }

        protected override void SetupData()
        {
            _connectionInfo = new ConnectionInfo("host", "user", new NoneAuthenticationMethod("userauth"));
        }

        protected override void SetupMocks()
        {
            var sequence = new MockSequence();

            ServiceFactoryMock.InSequence(sequence)
                               .Setup(p => p.CreateSocketFactory())
                               .Returns(SocketFactoryMock.Object);
            ServiceFactoryMock.InSequence(sequence)
                               .Setup(p => p.CreateSession(_connectionInfo, SocketFactoryMock.Object))
                               .Returns(SessionMock.Object);
            SessionMock.InSequence(sequence).Setup(p => p.Connect());
            _forwardedPortMock.InSequence(sequence).Setup(p => p.Start());
            SessionMock.InSequence(sequence).Setup(p => p.OnDisconnecting());
            _forwardedPortMock.InSequence(sequence).Setup(p => p.Stop());
            SessionMock.InSequence(sequence).Setup(p => p.Dispose());
        }

        protected override void Arrange()
        {
            base.Arrange();

            _sshClient = new SshClient(_connectionInfo, false, ServiceFactoryMock.Object);
            _sshClient.Connect();
            _sshClient.AddForwardedPort(_forwardedPortMock.Object);

            _forwardedPortMock.Object.Start();
        }

        protected override void Act()
        {
            _sshClient.Disconnect();
        }

        [TestMethod]
        public void ForwardedPortShouldBeStopped()
        {
            _forwardedPortMock.Verify(p => p.Stop(), Times.Once);
        }

        [TestMethod]
        public void ForwardedPortShouldBeRemovedFromSshClient()
        {
            Assert.IsFalse(_sshClient.ForwardedPorts.Any());
        }

        [TestMethod]
        public void DisconnectOnSessionShouldNeverBeInvoked()
        {
            SessionMock.Verify(p => p.Disconnect(), Times.Never);
        }

        [TestMethod]
        public void DisposeOnSessionShouldBeInvokedOnce()
        {
            SessionMock.Verify(p => p.Dispose(), Times.Once);
        }
    }
}
