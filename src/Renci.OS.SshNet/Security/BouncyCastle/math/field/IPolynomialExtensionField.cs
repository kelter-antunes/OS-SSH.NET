﻿namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.Field
{
    internal interface IPolynomialExtensionField
        : IExtensionField
    {
        IPolynomial MinimalPolynomial { get; }
    }
}