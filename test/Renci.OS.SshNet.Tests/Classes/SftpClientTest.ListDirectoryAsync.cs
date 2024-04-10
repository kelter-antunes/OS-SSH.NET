using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Tests.Properties;

namespace Renci.OS.SshNet.Tests.Classes
{
    /// <summary>
    /// Implementation of the SSH File Transfer Protocol (SFTP) over SSH.
    /// </summary>
    public partial class SftpClientTest
    {
        [TestMethod]
        [TestCategory("Sftp")]
        [ExpectedException(typeof(SshConnectionException))]
        public async Task Test_Sftp_ListDirectoryAsync_Without_ConnectingAsync()
        {
            using (var sftp = new SftpClient(Resources.HOST, Resources.USERNAME, Resources.PASSWORD))
            {
                await foreach (var file in sftp.ListDirectoryAsync(".", CancellationToken.None))
                {
                    Debug.WriteLine(file.FullName);
                }
            }
        }
    }
}
