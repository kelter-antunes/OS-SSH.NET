﻿using System;

namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.EC.Multiplier
{
    internal interface IPreCompCallback
    {
        PreCompInfo Precompute(PreCompInfo existing);
    }
}
