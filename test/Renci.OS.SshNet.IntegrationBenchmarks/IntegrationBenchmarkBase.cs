﻿using Renci.OS.SshNet.IntegrationTests.TestsFixtures;

namespace Renci.OS.SshNet.IntegrationBenchmarks
{
    public class IntegrationBenchmarkBase
    {
#pragma warning disable CA1822 // Mark members as static
        public async Task GlobalSetup()
        {
            await InfrastructureFixture.Instance.InitializeAsync().ConfigureAwait(false);
        }

        public async Task GlobalCleanup()
        {
            await InfrastructureFixture.Instance.DisposeAsync().ConfigureAwait(false);
        }
#pragma warning restore CA1822 // Mark members as static
    }
}
