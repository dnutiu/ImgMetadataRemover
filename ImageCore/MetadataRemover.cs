using ImageMagick;

namespace Image
{
    public class MetadataRemover : IMetadataRemover
    {
        private readonly ICompressor _compressor;
        private readonly IMagickImage _magickImage;

        public MetadataRemover(IMagickImage magickImage, ICompressor compressor)
        {
            _magickImage = magickImage;
            _compressor = compressor;
        }

        public void CleanImage(string newFileName)
        {
            _magickImage.RemoveProfile("exif");
            _magickImage.Write(newFileName);
            _compressor.Compress(newFileName);
        }
    }
}