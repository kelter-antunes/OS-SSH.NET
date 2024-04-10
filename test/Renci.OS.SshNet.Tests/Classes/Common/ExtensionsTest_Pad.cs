﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Tests.Classes.Common
{
    [TestClass]
    public class ExtensionsTest_Pad
    {
        [TestMethod]
        public void ShouldReturnNotPadded()
        {
            byte[] value = {0x0a, 0x0d};
            var padded = value.Pad(2);
            Assert.AreEqual(value, padded);
            Assert.AreEqual(value.Length, padded.Length);
        }

        [TestMethod]
        public void ShouldReturnPadded()
        {
            byte[] value = { 0x0a, 0x0d };
            var padded = value.Pad(3);
            Assert.AreEqual(value.Length + 1, padded.Length);
            Assert.AreEqual(0x00, padded[0]);
            Assert.AreEqual(0x0a, padded[1]);
            Assert.AreEqual(0x0d, padded[2]);
        }
    }
}