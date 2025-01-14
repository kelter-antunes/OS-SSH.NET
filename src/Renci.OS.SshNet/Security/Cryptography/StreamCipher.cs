﻿using System;

namespace Renci.OS.SshNet.Security.Cryptography
{
    /// <summary>
    /// Base class of stream cipher algorithms.
    /// </summary>
    public abstract class StreamCipher : SymmetricCipher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCipher"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        protected StreamCipher(byte[] key)
            : base(key)
        {
        }
    }
}
