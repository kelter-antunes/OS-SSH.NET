﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.OS.SshNet.Security.Cryptography.Ciphers;
using Renci.OS.SshNet.Security.Cryptography.Ciphers.Modes;
using Renci.OS.SshNet.Tests.Common;

using System.Linq;

namespace Renci.OS.SshNet.Tests.Classes.Security.Cryptography.Ciphers
{
    /// <summary>
    /// Implements CAST cipher algorithm
    /// </summary>
    [TestClass]
    public class CastCipherTest : TestBase
    {
        [TestMethod]
        public void Encrypt_128_CBC()
        {
            var input = new byte[] { 0x00, 0x00, 0x00, 0x2c, 0x1a, 0x05, 0x00, 0x00, 0x00, 0x0c, 0x73, 0x73, 0x68, 0x2d, 0x75, 0x73, 0x65, 0x72, 0x61, 0x75, 0x74, 0x68, 0x30, 0x9e, 0xe0, 0x9c, 0x12, 0xee, 0x3a, 0x30, 0x03, 0x52, 0x1c, 0x1a, 0xe7, 0x3e, 0x0b, 0x9a, 0xcf, 0x9a, 0x57, 0x42, 0x0b, 0x4f, 0x4a, 0x15, 0xa0, 0xf5 };
            var key = new byte[] { 0xe4, 0x94, 0xf9, 0xb1, 0x00, 0x4f, 0x16, 0x2a, 0x80, 0x11, 0xea, 0x73, 0x0d, 0xb9, 0xbf, 0x64 };
            var iv = new byte[] { 0x74, 0x8b, 0x4f, 0xe6, 0xc1, 0x29, 0xb3, 0x54, 0xec, 0x77, 0x92, 0xf3, 0x15, 0xa0, 0x41, 0xa8 };
            var output = new byte[] { 0x32, 0xef, 0xbd, 0xac, 0xb6, 0xfd, 0x1f, 0xae, 0x1b, 0x13, 0x5f, 0x31, 0x6d, 0x38, 0xcd, 0xb0, 0xe3, 0xca, 0xe1, 0xbc, 0xf8, 0xa7, 0xc2, 0x31, 0x62, 0x14, 0x3a, 0x9a, 0xda, 0xe3, 0xf8, 0xc8, 0x70, 0x87, 0x53, 0x21, 0x5d, 0xb7, 0x94, 0xb7, 0xe8, 0xc6, 0x9d, 0x46, 0x0c, 0x6d, 0x64, 0x6d };
            var testCipher = new CastCipher(key, new CbcCipherMode(iv), null);
            var r = testCipher.Encrypt(input);

            Assert.IsTrue(r.SequenceEqual(output));
        }

        [TestMethod]
        public void Decrypt_128_CBC()
        {
            var input = new byte[] { 0x00, 0x00, 0x00, 0x2c, 0x1a, 0x05, 0x00, 0x00, 0x00, 0x0c, 0x73, 0x73, 0x68, 0x2d, 0x75, 0x73, 0x65, 0x72, 0x61, 0x75, 0x74, 0x68, 0x30, 0x9e, 0xe0, 0x9c, 0x12, 0xee, 0x3a, 0x30, 0x03, 0x52, 0x1c, 0x1a, 0xe7, 0x3e, 0x0b, 0x9a, 0xcf, 0x9a, 0x57, 0x42, 0x0b, 0x4f, 0x4a, 0x15, 0xa0, 0xf5 };
            var key = new byte[] { 0xe4, 0x94, 0xf9, 0xb1, 0x00, 0x4f, 0x16, 0x2a, 0x80, 0x11, 0xea, 0x73, 0x0d, 0xb9, 0xbf, 0x64 };
            var iv = new byte[] { 0x74, 0x8b, 0x4f, 0xe6, 0xc1, 0x29, 0xb3, 0x54, 0xec, 0x77, 0x92, 0xf3, 0x15, 0xa0, 0x41, 0xa8 };
            var output = new byte[] { 0x32, 0xef, 0xbd, 0xac, 0xb6, 0xfd, 0x1f, 0xae, 0x1b, 0x13, 0x5f, 0x31, 0x6d, 0x38, 0xcd, 0xb0, 0xe3, 0xca, 0xe1, 0xbc, 0xf8, 0xa7, 0xc2, 0x31, 0x62, 0x14, 0x3a, 0x9a, 0xda, 0xe3, 0xf8, 0xc8, 0x70, 0x87, 0x53, 0x21, 0x5d, 0xb7, 0x94, 0xb7, 0xe8, 0xc6, 0x9d, 0x46, 0x0c, 0x6d, 0x64, 0x6d };
            var testCipher = new CastCipher(key, new CbcCipherMode(iv), null);
            var r = testCipher.Decrypt(output);

            Assert.IsTrue(r.SequenceEqual(input));
        }
    }
}
