namespace Renci.OS.SshNet.TestTools.OpenSSH.Formatters
{
    internal sealed class SubsystemFormatter
    {
        public string Format(Subsystem subsystem)
        {
            return subsystem.Name + " " + subsystem.Command;
        }
    }
}
