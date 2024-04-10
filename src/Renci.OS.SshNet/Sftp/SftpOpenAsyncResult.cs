using System;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp
{
    internal sealed class SftpOpenAsyncResult : AsyncResult<byte[]>
    {
        public SftpOpenAsyncResult(AsyncCallback asyncCallback, object state)
            : base(asyncCallback, state)
        {
        }
    }
}
