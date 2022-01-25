using System;
using Image;
using Xunit;

namespace ImageCore.Tests
{
    public class TestOriginalFilenameFileOutputPathFormatter
    {

        [Theory]
        [InlineData("", "./", @".\.jpg")]
        [InlineData("", @".\", @".\.jpg")]
        [InlineData("", "asd", @".\asd.jpg")]
        [InlineData("dir", "asd", @"dir\asd.jpg")]
        public void TestGetOutputPath(string directory, string file, string expectedPath)
        {
            var outputPathFormatter = OriginalFilenameFileOutputPathFormatter.Create(directory);
            Assert.Equal(expectedPath, outputPathFormatter.GetOutputPath(file));
        }

        [Fact]
        public void TestGetOutputPathNull()
        {
            var outputPathFormatter = OriginalFilenameFileOutputPathFormatter.Create("directory");
            Assert.Throws<ArgumentException>(() => outputPathFormatter.GetOutputPath(""));
        }
    }
}