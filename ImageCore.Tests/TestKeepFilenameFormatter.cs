using System;
using Image.Files;
using Xunit;

namespace ImageCore.Tests
{
    public class TestSimpleOutputSink
    {

        [Theory]
        [InlineData("", "./", @".\.jpg")]
        [InlineData("", @".\", @".\.jpg")]
        [InlineData("", "asd", @".\asd.jpg")]
        [InlineData("dir", "asd", @"dir\asd.jpg")]
        public void TestGetOutputPath(string directory, string file, string expectedPath)
        {
            var outputPathFormatter = SimpleOutputSink.Create(directory);
            Assert.Equal(expectedPath, outputPathFormatter.GetOutputPath(file));
        }

        [Fact]
        public void TestGetOutputPathNull()
        {
            var outputPathFormatter = SimpleOutputSink.Create("directory");
            Assert.Throws<ArgumentException>(() => outputPathFormatter.GetOutputPath(""));
        }
    }
}