﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Sftp;

namespace Renci.OS.SshNet.Tests.Classes.Sftp
{
    [TestClass]
    public class SftpFileStreamTest_OpenAsync_FileModeOpenOrCreate_FileAccessRead : SftpFileStreamAsyncTestBase
    {
        private Random _random;
        private string _path;
        private FileMode _fileMode;
        private FileAccess _fileAccess;
        private int _bufferSize;
        private uint _readBufferSize;
        private uint _writeBufferSize;
        private byte[] _handle;
        private SftpFileStream _target;
        private CancellationToken _cancellationToken;

        protected override void SetupData()
        {
            base.SetupData();

            _random = new Random();
            _path = _random.Next().ToString();
            _fileMode = FileMode.OpenOrCreate;
            _fileAccess = FileAccess.Read;
            _bufferSize = _random.Next(5, 1000);
            _readBufferSize = (uint) _random.Next(5, 1000);
            _writeBufferSize = (uint) _random.Next(5, 1000);
            _handle = GenerateRandom(_random.Next(1, 10), _random);
            _cancellationToken = new CancellationToken();
        }

        protected override void SetupMocks()
        {
            SftpSessionMock.InSequence(MockSequence)
                           .Setup(p => p.RequestOpenAsync(_path, Flags.Read | Flags.CreateNewOrOpen, _cancellationToken))
                           .ReturnsAsync(_handle);
            SftpSessionMock.InSequence(MockSequence)
                           .Setup(p => p.CalculateOptimalReadLength((uint) _bufferSize))
                           .Returns(_readBufferSize);
            SftpSessionMock.InSequence(MockSequence)
                           .Setup(p => p.CalculateOptimalWriteLength((uint) _bufferSize, _handle))
                           .Returns(_writeBufferSize);
        }

        protected override async Task ActAsync()
        {
            _target = await SftpFileStream.OpenAsync(SftpSessionMock.Object, _path, _fileMode, _fileAccess, _bufferSize, _cancellationToken);
        }

        [TestMethod]
        public void CanReadShouldReturnTrue()
        {
            Assert.IsTrue(_target.CanRead);
        }

        [TestMethod]
        public void CanSeekShouldReturnTrue()
        {
            Assert.IsTrue(_target.CanSeek);
        }

        [TestMethod]
        public void CanWriteShouldReturnFalse()
        {
            Assert.IsFalse(_target.CanWrite);
        }

        [TestMethod]
        public void CanTimeoutShouldReturnTrue()
        {
            Assert.IsTrue(_target.CanTimeout);
        }

        [TestMethod]
        public void PositionShouldReturnZero()
        {
            SftpSessionMock.InSequence(MockSequence).Setup(p => p.IsOpen).Returns(true);

            var actual = _target.Position;

            Assert.AreEqual(0L, actual);

            SftpSessionMock.Verify(p => p.IsOpen, Times.Exactly(1));
        }

        [TestMethod]
        public async Task ReadShouldStartReadingAtBeginningOfFile()
        {
            var buffer = new byte[8];
            var data = new byte[] { 5, 4, 3, 2, 1 };
            var expected = new byte[] { 0, 5, 4, 3, 2, 1, 0, 0 };

            SftpSessionMock.InSequence(MockSequence).Setup(p => p.IsOpen).Returns(true);
            SftpSessionMock.InSequence(MockSequence).Setup(p => p.RequestReadAsync(_handle, 0UL, _readBufferSize, _cancellationToken)).ReturnsAsync(data);

            var actual = await _target.ReadAsync(buffer, 1, data.Length);

            Assert.AreEqual(data.Length, actual);
            Assert.IsTrue(buffer.IsEqualTo(expected));

            SftpSessionMock.Verify(p => p.IsOpen, Times.Exactly(1));
            SftpSessionMock.Verify(p => p.RequestReadAsync(_handle, 0UL, _readBufferSize, _cancellationToken), Times.Once);
        }

        [TestMethod]
        public async Task WriteShouldThrowNotSupportedException()
        {
            var buffer = new byte[_writeBufferSize];

            SftpSessionMock.InSequence(MockSequence).Setup(p => p.IsOpen).Returns(true);

            try
            {
                await _target.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (NotSupportedException ex)
            {
                Assert.IsNull(ex.InnerException);
            }

            SftpSessionMock.Verify(p => p.IsOpen, Times.Exactly(1));
        }

        [TestMethod]
        public void RequestOpenOnSftpSessionShouldBeInvokedOnce()
        {
            SftpSessionMock.Verify(p => p.RequestOpenAsync(_path, Flags.Read | Flags.CreateNewOrOpen, _cancellationToken), Times.Once);
        }
    }
}