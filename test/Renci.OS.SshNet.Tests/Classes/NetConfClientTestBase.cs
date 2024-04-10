using Moq;
using Renci.OS.SshNet.NetConf;

namespace Renci.OS.SshNet.Tests.Classes
{
    public abstract class NetConfClientTestBase : BaseClientTestBase
    {
        internal Mock<INetConfSession> NetConfSessionMock { get; private set; }

        protected override void CreateMocks()
        {
            base.CreateMocks();

            NetConfSessionMock = new Mock<INetConfSession>(MockBehavior.Strict);
        }
    }
}
