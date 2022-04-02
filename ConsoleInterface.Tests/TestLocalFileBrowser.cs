using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace ConsoleInterface.Tests;

public class TestLocalFileBrowser
{
    private readonly string? _testsProjectDirectory;

    public TestLocalFileBrowser()
    {
        _testsProjectDirectory = Environment.GetEnvironmentVariable("IMAGE_CORE_TESTS");
        if (_testsProjectDirectory == null) throw new Exception("Environment variable IMAGE_CORE_TESTS is not set!");
    }

    [Fact]
    public void TestGetFilenamesFromPath_DirectoryNotFound()
    {
        var filesRetriever = LocalFileBrowser.Create();
        Assert.Throws<DirectoryNotFoundException>(() => filesRetriever.GetFilenamesFromPath("a"));
    }

    [Fact]
    public void TestGetFilenamesFromPath()
    {
        var filesRetriever = LocalFileBrowser.Create();
        Debug.Assert(_testsProjectDirectory != null, nameof(_testsProjectDirectory) + " != null");
        var filePaths = filesRetriever.GetFilenamesFromPath(Path.Combine(_testsProjectDirectory, "test_pictures"));
        Assert.NotNull(filePaths);
        var filePathsList = filePaths.ToList();
        var expectedFileNames = new List<string>
        {
            "IMG_0138.HEIC", "IMG_0138.jpg", "IMG_0140.HEIC"
        };

        Assert.NotEmpty(filePathsList);
        for (var i = 0; i < filePathsList.Count; i++)
            Assert.Equal(expectedFileNames[i], Path.GetFileName(filePathsList[i]));
    }
}