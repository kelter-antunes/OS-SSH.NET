﻿using System;

namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.EC
{
    internal interface ECLookupTable
    {
        int Size { get; }
        ECPoint Lookup(int index);
    }
}