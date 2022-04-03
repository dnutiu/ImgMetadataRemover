using System.IO;
using ImageMagick;

namespace Image.Core
{
    /// <summary>
    ///     ExifRemoverAndCompressor removes exif metadata from an image.
    /// </summary>
    public class ExifRemoverAndCompressor : IMetadataRemover
    {
        private readonly ICompressor _compressor;
        private readonly IMagickImage _magickImage;

        /// <summary>
        ///     Constructs an instance of ExifRemoverAndCompressor.
        /// </summary>
        /// <param name="magickImage">MagicImage instance.</param>
        /// <param name="compressor">Compressor instance.</param>
        public ExifRemoverAndCompressor(IMagickImage magickImage, ICompressor compressor)
        {
            _magickImage = magickImage;
            _compressor = compressor;
        }

        /// <summary>
        ///     Cleans the image.
        /// </summary>
        public void CleanImage()
        {
            _magickImage.RemoveProfile("exif");
        }

        /// <summary>
        ///     Save the image under a new file path.
        /// </summary>
        /// <param name="newFilePath">The new path of the image.</param>
        public void SaveImage(string newFilePath)
        {
            _magickImage.Write(newFilePath);
            _compressor.Compress(newFilePath);
        }

        /// <inheritdoc />
        public string GetImagePath()
        {
            return _magickImage.FileName;
        }

        /// <summary>
        ///     Saves the image.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void SaveImage(Stream stream)
        {
            _magickImage.Write(stream);
            _compressor.Compress(stream);
        }
    }
}