using BenchmarkDotNet.Running;

namespace Renci.OS.SshNet.IntegrationBenchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
