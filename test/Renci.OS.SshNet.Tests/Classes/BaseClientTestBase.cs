using Moq;
using Renci.OS.SshNet.Connection;
using Renci.OS.SshNet.Tests.Common;

namespace Renci.OS.SshNet.Tests.Classes
{
    public abstract class BaseClientTestBase : TripleATestBase
    {
        internal Mock<IServiceFactory> ServiceFactoryMock { get; private set; }
        internal Mock<ISocketFactory> SocketFactoryMock { get; private set; }
        internal Mock<ISession> SessionMock { get; private set; }

        protected virtual void CreateMocks()
        {
            ServiceFactoryMock = new Mock<IServiceFactory>(MockBehavior.Strict);
            SocketFactoryMock = new Mock<ISocketFactory>(MockBehavior.Strict);
            SessionMock = new Mock<ISession>(MockBehavior.Strict);
        }

        protected virtual void SetupData()
        {
        }

        protected virtual void SetupMocks()
        {
        }

        protected override void Arrange()
        {
            CreateMocks();
            SetupData();
            SetupMocks();
        }
    }
}
