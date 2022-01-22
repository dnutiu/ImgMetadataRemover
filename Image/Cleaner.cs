using ImageMagick;

namespace Image
{
    public class Cleaner : ICleaner
    {
        private readonly ICompressor _compressor;
        private readonly IMagickImage _magickImage;

        public Cleaner(IMagickImage magickImage, ICompressor compressor)
        {
            _magickImage = magickImage;
            _compressor = compressor;
        }

        public Cleaner(string fileName) : this(new MagickImage(fileName), new LosslessCompressor())
        {
        }

        public void CleanImage(string newFileName)
        {
            _magickImage.RemoveProfile("exif");
            _magickImage.Write(newFileName);
            _compressor.Compress(newFileName);
        }

        public void RemoveExifData(string newFilename)
        {
            _magickImage.RemoveProfile("exif");
            _magickImage.Write(newFilename);
        }

        public void OptimizeImage(string fileName)
        {
            _compressor.Compress(fileName);
        }
    }
}