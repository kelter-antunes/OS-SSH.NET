using System;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp
{
    internal sealed class SFtpStatAsyncResult : AsyncResult<SftpFileAttributes>
    {
        public SFtpStatAsyncResult(AsyncCallback asyncCallback, object state)
            : base(asyncCallback, state)
        {
        }
    }
}
