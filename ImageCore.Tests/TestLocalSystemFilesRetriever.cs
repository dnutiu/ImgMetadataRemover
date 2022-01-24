using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Image;
using Xunit;

namespace ImageCore.Tests
{
    public class TestLocalSystemFilesRetriever
    {
        private readonly string _testsProjectDirectory;

        public TestLocalSystemFilesRetriever()
        {
            _testsProjectDirectory = Environment.GetEnvironmentVariable("IMAGE_CORE_TESTS");
        }

        [Fact]
        public void TestGetFilenamesFromPath_DirectoryNotFound()
        {
            var filesRetriever = LocalSystemFilesRetriever.Create();
            Assert.Throws<DirectoryNotFoundException>(() => filesRetriever.GetFilenamesFromPath("a"));
        }

        [Fact]
        public void TestGetFilenamesFromPath()
        {
            var filesRetriever = LocalSystemFilesRetriever.Create();
            var filePaths = filesRetriever.GetFilenamesFromPath(Path.Join(_testsProjectDirectory, "test_pictures"));
            Assert.NotNull(filePaths);
            var filePathsList = filePaths.ToList();
            var expectedFileNames = new List<string>
            {
                "IMG_0138.HEIC", "IMG_0140.HEIC",
            };

            Assert.NotEmpty(filePathsList);
            for (var i = 0; i < filePathsList.Count; i++)
            {
                Assert.Equal(expectedFileNames[i], Path.GetFileName(filePathsList[i]));
            }
        }
    }
}