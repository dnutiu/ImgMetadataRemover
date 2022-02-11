using ImageMagick;

namespace Image.Core
{
    /// <summary>
    ///     MetadataRemover removes metadata from an image. The exif profile.
    /// </summary>
    public class ExifMetadataRemoverAndCompressor : IMetadataRemover
    {
        private readonly ICompressor _compressor;
        private readonly IMagickImage _magickImage;

        /// <summary>
        ///     Constructs an instance of MetadataRemover.
        /// </summary>
        /// <param name="magickImage">MagicImage instance.</param>
        /// <param name="compressor">Compressor instance.</param>
        public ExifMetadataRemoverAndCompressor(IMagickImage magickImage, ICompressor compressor)
        {
            _magickImage = magickImage;
            _compressor = compressor;
        }

        /// <summary>
        ///     Cleans the images and compresses it.
        /// </summary>
        /// <param name="newFilePath">The file path to save the clean image.</param>
        public void CleanImage(string newFilePath)
        {
            _magickImage.RemoveProfile("exif");
            _magickImage.Write(newFilePath);
            _compressor.Compress(newFilePath);
        }
    }
}