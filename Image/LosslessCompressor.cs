using ImageMagick;

namespace Image
{
    public class LosslessCompressor : ICompressor
    {
        private readonly ImageOptimizer _imageOptimizer;

        public LosslessCompressor()
        {
            _imageOptimizer = new ImageOptimizer();
        }

        public void Compress(string fileName)
        {
            _imageOptimizer.LosslessCompress(fileName);
        }
    }
}