using System;

using Renci.OS.SshNet.Common;

namespace Renci.OS.SshNet.Sftp
{
    /// <summary>
    /// Reads a given file.
    /// </summary>
    internal interface ISftpFileReader : IDisposable
    {
        /// <summary>
        /// Reads a sequence of bytes from the current file and advances the position within the file by the number of bytes read.
        /// </summary>
        /// <returns>
        /// The sequence of bytes read from the file, or a zero-length array if the end of the file
        /// has been reached.
        /// </returns>
        /// <exception cref="ObjectDisposedException">The current <see cref="ISftpFileReader"/> is disposed.</exception>
        /// <exception cref="SshException">Attempting to read beyond the end of the file.</exception>
        byte[] Read();
    }
}
