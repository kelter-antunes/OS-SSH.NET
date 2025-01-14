﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.OS.SshNet.Tests.Properties;
using System;
using System.Threading.Tasks;

namespace Renci.OS.SshNet.Tests.Classes
{
    /// <summary>
    /// Implementation of the SSH File Transfer Protocol (SFTP) over SSH.
    /// </summary>
    public partial class SftpClientTest
    {
        [TestMethod]
        [TestCategory("Sftp")]
        [Description("Test passing null to DeleteFile.")]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_Sftp_DeleteFileAsync_Null()
        {
            using (var sftp = new SftpClient(Resources.HOST, Resources.USERNAME, Resources.PASSWORD))
            {
                await sftp.DeleteFileAsync(null, default);
            }
        }
    }
}