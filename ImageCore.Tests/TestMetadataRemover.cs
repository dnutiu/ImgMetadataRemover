using Image.Core;
using ImageMagick;
using Moq;
using Xunit;

namespace ImageCore.Tests
{
    public class TestMetadataRemover
    {
        [Fact]
        public void TestExifRemoverAndCompressorCleanImage()
        {
            // Setup
            var magicImageMock = new Mock<IMagickImage>();
            var compressorMock = new Mock<ICompressor>();
            var metadataRemover = new ExifRemoverAndCompressor(magicImageMock.Object, compressorMock.Object);

            // Test
            metadataRemover.CleanImage("path");

            // Assert
            magicImageMock.Verify(i => i.RemoveProfile("exif"));
            magicImageMock.Verify(i => i.Write("path"));
            compressorMock.Verify(i => i.Compress("path"));
        }

        [Fact]
        public void TestExifRemoverAndCompressorGetImagePath()
        {
            // Setup
            var magicImageMock = new Mock<IMagickImage>();
            magicImageMock.Setup(i => i.FileName).Returns("P4th");

            var compressorMock = new Mock<ICompressor>();
            var metadataRemover = new ExifRemoverAndCompressor(magicImageMock.Object, compressorMock.Object);

            // Test
            var result = metadataRemover.GetImagePath();

            // Assert
            Assert.Equal("P4th", result);
        }
    }
}