namespace Image.Compressor
{
    /// <summary>
    ///     Does nothing. Using this Compressor will have no effect.
    /// </summary>
    public class NullCompressor : ICompressor
    {
        public static readonly NullCompressor Instance;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public void Compress(string fileName)
        {
        }
    }
}