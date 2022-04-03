using System.IO;
using ImageMagick;

namespace Image.Core
{
    /// <summary>
    ///     LosslessCompressor compresses an image using lossless compression provided by ImageMagick.
    /// </summary>
    public class LosslessCompressor : ICompressor
    {
        public static readonly LosslessCompressor Instance = new LosslessCompressor();
        private readonly ImageOptimizer _imageOptimizer;

        public LosslessCompressor()
        {
            _imageOptimizer = new ImageOptimizer();
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public void Compress(string fileName)
        {
            _imageOptimizer.LosslessCompress(fileName);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public void Compress(Stream stream)
        {
            _imageOptimizer.LosslessCompress(stream);
        }
    }
}