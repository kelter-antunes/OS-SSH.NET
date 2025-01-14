﻿using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Messages.Transport;

namespace Renci.OS.SshNet.Tests.Classes.Messages.Transport
{
    public class KeyExchangeDhGroupExchangeGroupBuilder
    {
        private BigInteger _safePrime;
        private BigInteger _subGroup;

        public KeyExchangeDhGroupExchangeGroupBuilder WithSafePrime(BigInteger safePrime)
        {
            _safePrime = safePrime;
            return this;
        }

        public KeyExchangeDhGroupExchangeGroupBuilder WithSubGroup(BigInteger subGroup)
        {
            _subGroup = subGroup;
            return this;
        }

        public byte[] Build()
        {
            var sshDataStream = new SshDataStream(0);
            var target = new KeyExchangeDhGroupExchangeGroup();
            sshDataStream.WriteByte(target.MessageNumber);
            sshDataStream.Write(_safePrime);
            sshDataStream.Write(_subGroup);
            return sshDataStream.ToArray();
        }
    }
}
