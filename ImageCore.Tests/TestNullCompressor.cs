﻿using System;
using System.IO;
using Image.Core;
using Xunit;

namespace ImageCore.Tests
{
    public class TestNullCompressor
    {
        private readonly string _testsProjectDirectory;

        public TestNullCompressor()
        {
            _testsProjectDirectory = Environment.GetEnvironmentVariable("IMAGE_CORE_TESTS");
        }


        [Fact]
        public void TestNullCompressor_Compress()
        {
            ICompressor compressor = new NullCompressor();
            var sourceFileName = Path.Combine(_testsProjectDirectory, "test_pictures/IMG_0138.HEIC");
            var destinationFileName = Path.GetTempFileName();
            File.Copy(sourceFileName, destinationFileName, true);
            compressor.Compress(destinationFileName);

            var originalFile = File.Open(sourceFileName, FileMode.Open);
            var compressedFile = File.Open(destinationFileName, FileMode.Open);

            Assert.True(compressedFile.Length == originalFile.Length);

            originalFile.Close();
            compressedFile.Close();
            File.Delete(destinationFileName);
        }
    }
}