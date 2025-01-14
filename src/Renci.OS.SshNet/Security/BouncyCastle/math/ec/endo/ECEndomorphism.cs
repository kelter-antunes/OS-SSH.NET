﻿using System;

namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.EC.Endo
{
    internal interface ECEndomorphism
    {
        ECPointMap PointMap { get; }

        bool HasEfficientPointMap { get; }
    }
}
