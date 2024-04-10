using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp.Responses
{
    internal abstract class ExtendedReplyInfo
    {
        public abstract void LoadData(SshDataStream stream);
    }
}
