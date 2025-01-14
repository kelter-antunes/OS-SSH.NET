﻿using System;

namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.EC.Endo
{
    internal interface GlvEndomorphism
        : ECEndomorphism
    {
        BigInteger[] DecomposeScalar(BigInteger k);
    }
}
