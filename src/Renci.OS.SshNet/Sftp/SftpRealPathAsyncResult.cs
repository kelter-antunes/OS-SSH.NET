using System;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp
{
    internal sealed class SftpRealPathAsyncResult : AsyncResult<string>
    {
        public SftpRealPathAsyncResult(AsyncCallback asyncCallback, object state)
            : base(asyncCallback, state)
        {
        }
    }
}
