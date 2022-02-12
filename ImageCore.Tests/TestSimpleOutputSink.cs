using System;
using System.IO;
using Image.Core;
using Image.Files;
using Moq;
using Xunit;

namespace ImageCore.Tests
{
    public class TestSimpleOutputSink
    {
        private readonly string _testsProjectDirectory;

        public TestSimpleOutputSink()
        {
            _testsProjectDirectory = Environment.GetEnvironmentVariable("IMAGE_CORE_TESTS");
        }

        [Theory]
        [InlineData("", "./", @".\.jpg")]
        [InlineData("", @".\", @".\.jpg")]
        [InlineData("", "asd", @".\asd.jpg")]
        [InlineData("dir", "asd", @"dir\asd.jpg")]
        public void TestGetOutputPath(string directory, string file, string expectedPath)
        {
            var sink = SimpleOutputSink.Create(directory);
            Assert.Equal(expectedPath, sink.GetOutputPath(file));
        }

        [Fact]
        public void TestGetOutputPathNull()
        {
            var sink = SimpleOutputSink.Create("directory");
            Assert.Throws<ArgumentException>(() => sink.GetOutputPath(""));
        }

        [Fact]
        public void TestSave()
        {
            // Setup
            var sink = SimpleOutputSink.Create("directory");
            var metadataRemoverMock = new Mock<IMetadataRemover>();
            metadataRemoverMock.Setup(i => i.GetImagePath()).Returns("alo.wtf");

            // Test
            sink.Save(metadataRemoverMock.Object);

            // Assert
            metadataRemoverMock.Verify(i => i.CleanImage("directory\\alo.jpg"));
        }

        [Fact]
        public void TestSaveFileExists()
        {
            // Setup
            var sink = SimpleOutputSink.Create(Path.Join(_testsProjectDirectory, "test_pictures"));
            var metadataRemoverMock = new Mock<IMetadataRemover>();
            var sourceFileName = Path.Join(_testsProjectDirectory, "test_pictures\\IMG_0138.HEIC");
            metadataRemoverMock.Setup(i => i.GetImagePath()).Returns(sourceFileName);

            // Test
            sink.Save(metadataRemoverMock.Object);

            // Assert
            metadataRemoverMock.Verify(i => i.CleanImage(It.IsAny<string>()), Times.Never);
        }
    }
}