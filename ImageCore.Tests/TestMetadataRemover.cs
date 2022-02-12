using Image.Core;
using ImageMagick;
using Moq;
using Xunit;

namespace ImageCore.Tests
{
    public class TestMetadataRemover
    {
        [Fact]
        public void TestCleanImage()
        {
            // Setup
            var magicImageMock = new Mock<IMagickImage>();
            var compressorMock = new Mock<ICompressor>();
            var metadataRemover = new ExifRemoverAndCompressor(magicImageMock.Object, compressorMock.Object);
            
            // Test
            metadataRemover.CleanImage("path");
            
            // Assert
            magicImageMock.Verify( i => i.RemoveProfile("exif"));
            magicImageMock.Verify( i => i.Write("path"));
            compressorMock.Verify( i => i.Compress("path"));
        }
    }
}