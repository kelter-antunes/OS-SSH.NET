﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Sftp;
using System;
using System.Threading;

namespace Renci.OS.SshNet.Tests.Classes.Sftp
{
    public abstract class SftpFileReaderTestBase
    {
        internal Mock<ISftpSession> SftpSessionMock {  get; private set;}

        protected abstract void SetupData();

        protected void CreateMocks()
        {
            SftpSessionMock = new Mock<ISftpSession>(MockBehavior.Strict);
        }

        protected abstract void SetupMocks();

        protected virtual void Arrange()
        {
            SetupData();
            CreateMocks();
            SetupMocks();
        }

        [TestInitialize]
        public void SetUp()
        {
            Arrange();
            Act();
        }

        protected abstract void Act();

        protected static SftpFileAttributes CreateSftpFileAttributes(long size)
        {
            var utcDefault = DateTime.SpecifyKind(default, DateTimeKind.Utc);
            return new SftpFileAttributes(utcDefault, utcDefault, size, default, default, default, null);
        }

        protected static byte[] CreateByteArray(Random random, int length)
        {
            var chunk = new byte[length];
            random.NextBytes(chunk);
            return chunk;
        }

        protected static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
        {
            var result = WaitHandle.WaitAny(waitHandles, millisecondsTimeout);

            if (result == WaitHandle.WaitTimeout)
            {
                throw new SshOperationTimeoutException();
            }

            return result;
        }
    }
}
