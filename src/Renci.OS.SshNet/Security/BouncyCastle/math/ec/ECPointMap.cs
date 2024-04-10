using System;

namespace Renci.OS.SshNet.Security.Org.BouncyCastle.Math.EC
{
    internal interface ECPointMap
    {
        ECPoint Map(ECPoint p);
    }
}
