namespace Image.Compressor
{
    /// <summary>
    ///     ICompressor is an interface for implementing image compressors.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        ///     The method compresses an image in place.
        /// </summary>
        /// <param name="fileName">The file name of the image to be compressed.</param>
        void Compress(string fileName);
    }
}