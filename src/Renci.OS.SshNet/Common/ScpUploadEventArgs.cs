﻿using System;

namespace Renci.OS.SshNet.Common
{
    /// <summary>
    /// Provides data for the Uploading event.
    /// </summary>
    public class ScpUploadEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScpUploadEventArgs"/> class.
        /// </summary>
        /// <param name="filename">The uploaded filename.</param>
        /// <param name="size">The uploaded file size.</param>
        /// <param name="uploaded">The number of uploaded bytes so far.</param>
        public ScpUploadEventArgs(string filename, long size, long uploaded)
        {
            Filename = filename;
            Size = size;
            Uploaded = uploaded;
        }

        /// <summary>
        /// Gets the uploaded filename.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Gets the uploaded file size.
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// Gets number of uploaded bytes so far.
        /// </summary>
        public long Uploaded { get; }
    }
}
