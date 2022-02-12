using System.IO;
using Ardalis.GuardClauses;
using Image.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image.Files
{
    /// <summary>
    ///     SimpleOutputFormatter keeps the original file name of the image when formatting it.
    ///     SimpleOutputFormatter also saves all the file names into a new directory.
    ///     path.
    /// </summary>
    public class SimpleOutputSink : IOutputSink
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly string _outputDirectory;

        /// <summary>
        ///     Creates an instance of SimpleOutputFormatter.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public SimpleOutputSink(string outputDirectory)
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

        /// <summary>
        ///     Returns a path containing the file name in the output directory.
        /// </summary>
        /// <param name="initialFilePath">The initial path of the image.</param>
        /// <returns>An absolute path of the form output_directory/initialFileName.jpg</returns>
        public string GetOutputPath(string initialFilePath)
        {
            Logger.LogDebug($"KeepFilenameFormatter - {_outputDirectory} - {initialFilePath}");
            Guard.Against.NullOrEmpty(initialFilePath, nameof(initialFilePath));
            var fileName = Path.GetFileName(initialFilePath).Split('.')[0];
            var path = Path.Combine(_outputDirectory, $"{fileName}.jpg");
            return path;
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
            metadataRemover.CleanImage(newFilePath);
            return true;
        }

        /// <summary>
        ///     Creates an instance of OriginalFilenameFileOutputPathFormatter.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public static SimpleOutputSink Create(string outputDirectory)
        {
            return new SimpleOutputSink(outputDirectory);
        }
    }
}