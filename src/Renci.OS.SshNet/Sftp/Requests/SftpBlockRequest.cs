﻿using System;

using Renci.OS.SshNet.Sftp.Responses;

namespace Renci.OS.SshNet.Sftp.Requests
{
    internal sealed class SftpBlockRequest : SftpRequest
    {
        public override SftpMessageTypes SftpMessageType
        {
            get { return SftpMessageTypes.Block; }
        }

        public byte[] Handle { get; private set; }

        public ulong Offset { get; private set; }

        public ulong Length { get; private set; }

        public uint LockMask { get; private set; }

        /// <summary>
        /// Gets the size of the message in bytes.
        /// </summary>
        /// <value>
        /// The size of the messages in bytes.
        /// </value>
        protected override int BufferCapacity
        {
            get
            {
                var capacity = base.BufferCapacity;
                capacity += 4; // Handle length
                capacity += Handle.Length; // Handle
                capacity += 8; // Offset
                capacity += 8; // Length
                capacity += 4; // LockMask
                return capacity;
            }
        }

        public SftpBlockRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, ulong length, uint lockMask, Action<SftpStatusResponse> statusAction)
            : base(protocolVersion, requestId, statusAction)
        {
            Handle = handle;
            Offset = offset;
            Length = length;
            LockMask = lockMask;
        }

        protected override void LoadData()
        {
            base.LoadData();

            Handle = ReadBinary();
            Offset = ReadUInt64();
            Length = ReadUInt64();
            LockMask = ReadUInt32();
        }

        protected override void SaveData()
        {
            base.SaveData();

            WriteBinaryString(Handle);
            Write(Offset);
            Write(Length);
            Write(LockMask);
        }
    }
}
