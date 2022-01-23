﻿namespace Image
{
    /// <summary>
    ///     Does nothing. Using this Compressor will have no effect.
    /// </summary>
    public class NullCompressor : ICompressor
    {
        public static readonly NullCompressor Instance = new NullCompressor();

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public void Compress(string fileName)
        {
        }
    }
}