﻿using System;

using Renci.OS.SshNet.Common;
using Renci.OS.SshNet.Security.Cryptography;

namespace Renci.OS.SshNet.Security
{
    /// <summary>
    /// Contains the RSA private and public key.
    /// </summary>
    public class RsaKey : Key, IDisposable
    {
        private bool _isDisposed;
        private RsaDigitalSignature _digitalSignature;

        /// <summary>
        /// Gets the name of the key.
        /// </summary>
        /// <returns>
        /// The name of the key.
        /// </returns>
        public override string ToString()
        {
            return "ssh-rsa";
        }

        /// <summary>
        /// Gets the modulus.
        /// </summary>
        /// <value>
        /// The modulus.
        /// </value>
        public BigInteger Modulus { get; }

        /// <summary>
        /// Gets the exponent.
        /// </summary>
        /// <value>
        /// The exponent.
        /// </value>
        public BigInteger Exponent { get; }

        /// <summary>
        /// Gets the D.
        /// </summary>
        /// <value>
        /// The D.
        /// </value>
        public BigInteger D { get; }

        /// <summary>
        /// Gets the P.
        /// </summary>
        /// <value>
        /// The P.
        /// </value>
        public BigInteger P { get; }

        /// <summary>
        /// Gets the Q.
        /// </summary>
        /// <value>
        /// The Q.
        /// </value>
        public BigInteger Q { get; }

        /// <summary>
        /// Gets the DP.
        /// </summary>
        /// <value>
        /// The DP.
        /// </value>
        public BigInteger DP { get; }

        /// <summary>
        /// Gets the DQ.
        /// </summary>
        /// <value>
        /// The DQ.
        /// </value>
        public BigInteger DQ { get; }

        /// <summary>
        /// Gets the inverse Q.
        /// </summary>
        /// <value>
        /// The inverse Q.
        /// </value>
        public BigInteger InverseQ { get; }

        /// <summary>
        /// Gets the length of the key.
        /// </summary>
        /// <value>
        /// The length of the key.
        /// </value>
        public override int KeyLength
        {
            get
            {
                return Modulus.BitLength;
            }
        }

        /// <summary>
        /// Gets the digital signature implementation for this key.
        /// </summary>
        /// <returns>
        /// An implementation of an RSA digital signature using the SHA-1 hash algorithm.
        /// </returns>
        protected internal override DigitalSignature DigitalSignature
        {
            get
            {
                _digitalSignature ??= new RsaDigitalSignature(this);

                return _digitalSignature;
            }
        }

        /// <summary>
        /// Gets the RSA public key.
        /// </summary>
        /// <value>
        /// An array with <see cref="Exponent"/> at index 0, and <see cref="Modulus"/>
        /// at index 1.
        /// </value>
        public override BigInteger[] Public
        {
            get
            {
                return new[] { Exponent, Modulus };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaKey"/> class.
        /// </summary>
        /// <param name="publicKeyData">The encoded public key data.</param>
        public RsaKey(SshKeyData publicKeyData)
        {
            if (publicKeyData is null)
            {
                throw new ArgumentNullException(nameof(publicKeyData));
            }

            if (publicKeyData.Name != "ssh-rsa" || publicKeyData.Keys.Length != 2)
            {
                throw new ArgumentException($"Invalid RSA public key data. ({publicKeyData.Name}, {publicKeyData.Keys.Length}).", nameof(publicKeyData));
            }

            Exponent = publicKeyData.Keys[0];
            Modulus = publicKeyData.Keys[1];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaKey"/> class.
        /// </summary>
        /// <param name="privateKeyData">DER encoded private key data.</param>
        public RsaKey(byte[] privateKeyData)
        {
            if (privateKeyData is null)
            {
                throw new ArgumentNullException(nameof(privateKeyData));
            }

            var der = new DerData(privateKeyData);
            _ = der.ReadBigInteger(); // skip version

            Modulus = der.ReadBigInteger();
            Exponent = der.ReadBigInteger();
            D = der.ReadBigInteger();
            P = der.ReadBigInteger();
            Q = der.ReadBigInteger();
            DP = der.ReadBigInteger();
            DQ = der.ReadBigInteger();
            InverseQ = der.ReadBigInteger();

            if (!der.IsEndOfData)
            {
                throw new InvalidOperationException("Invalid private key (expected EOF).");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaKey"/> class.
        /// </summary>
        /// <param name="modulus">The modulus.</param>
        /// <param name="exponent">The exponent.</param>
        /// <param name="d">The d.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="inverseQ">The inverse Q.</param>
        public RsaKey(BigInteger modulus, BigInteger exponent, BigInteger d, BigInteger p, BigInteger q, BigInteger inverseQ)
        {
            Modulus = modulus;
            Exponent = exponent;
            D = d;
            P = p;
            Q = q;
            DP = PrimeExponent(d, p);
            DQ = PrimeExponent(d, q);
            InverseQ = inverseQ;
        }

        private static BigInteger PrimeExponent(BigInteger privateExponent, BigInteger prime)
        {
            var pe = prime - BigInteger.One;
            return privateExponent % pe;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                var digitalSignature = _digitalSignature;
                if (digitalSignature != null)
                {
                    digitalSignature.Dispose();
                    _digitalSignature = null;
                }

                _isDisposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="RsaKey"/> is reclaimed by garbage collection.
        /// </summary>
        ~RsaKey()
        {
            Dispose(disposing: false);
        }
    }
}
