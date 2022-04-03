using System;
using System.IO;
using Image;
using Image.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleInterface
{
    /// <summary>
    ///     DirectoryOutputSink keeps the original file name of the image when formatting it.
    ///     DirectoryOutputSink also saves all the file names into a new directory.
    ///     path.
    /// </summary>
    public class DirectoryOutputSink : IOutputSink
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly string _outputDirectory;

        /// <summary>
        ///     Creates an instance of DirectoryOutputSink.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public DirectoryOutputSink(string outputDirectory)
        {
            if (outputDirectory.Equals(""))
            {
                outputDirectory = ".";
            }
            else
            {
                FileSystemHelpers.CreateDestinationDirectory(outputDirectory);
            }

            _outputDirectory = outputDirectory;
        }

        public bool Save(IMetadataRemover metadataRemover)
        {
            var newFilePath = GetOutputPath(metadataRemover.GetImagePath());
            var fileExists = FileSystemHelpers.CheckIfFileExists(newFilePath);
            if (fileExists)
            {
                Logger.LogWarning($"File {newFilePath} exists, skipping");
                return false;
            }

            // Save the image under the same name in the new directory.
            metadataRemover.CleanImage();
            metadataRemover.SaveImage(newFilePath);
            return true;
        }

        /// <summary>
        ///     Returns a path containing the file name in the output directory.
        /// </summary>
        /// <param name="initialFilePath">The initial path of the image.</param>
        /// <returns>An absolute path of the form output_directory/initialFileName.jpg</returns>
        public string GetOutputPath(string initialFilePath)
        {
            Logger.LogDebug($"KeepFilenameFormatter - {_outputDirectory} - {initialFilePath}");
            if (string.IsNullOrEmpty(initialFilePath))
            {
                throw new ArgumentException("The output file path cannot be null or empty!");
            }

            var fileName = Path.GetFileName(initialFilePath).Split('.')[0];
            var path = Path.Combine(_outputDirectory, $"{fileName}.jpg");
            return path;
        }

        /// <summary>
        ///     Creates an instance of DirectoryOutputSink.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public static DirectoryOutputSink Create(string outputDirectory)
        {
            return new DirectoryOutputSink(outputDirectory);
        }
    }
}