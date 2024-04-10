using System;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp
{
    internal sealed class SftpReadAsyncResult : AsyncResult<byte[]>
    {
        public SftpReadAsyncResult(AsyncCallback asyncCallback, object state)
            : base(asyncCallback, state)
        {
        }
    }
}
