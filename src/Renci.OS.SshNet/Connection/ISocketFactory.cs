﻿using System.Net.Sockets;

namespace Renci.OS.SshNet.Connection
{
    /// <summary>
    /// Represents a factory to create <see cref="Socket"/> instances.
    /// </summary>
    internal interface ISocketFactory
    {
        /// <summary>
        /// Creates a <see cref="Socket"/> with the specified <see cref="AddressFamily"/>,
        /// <see cref="SocketType"/> and <see cref="ProtocolType"/> that does not use the
        /// <c>Nagle</c> algorithm.
        /// </summary>
        /// <param name="addressFamily">The <see cref="AddressFamily"/>.</param>
        /// <param name="socketType">The <see cref="SocketType"/>.</param>
        /// <param name="protocolType">The <see cref="ProtocolType"/>.</param>
        /// <returns>
        /// The <see cref="Socket"/>.
        /// </returns>
        Socket Create(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType);
    }
}
