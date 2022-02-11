using System.IO;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image.Files
{
    /// <summary>
    ///     KeepFilenameFormatter keeps the original file name of the image when formatting the new output
    ///     path.
    /// </summary>
    public class KeepFilenameFormatter : IFileOutputFormatter
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly string _outputDirectory;

        /// <summary>
        ///     Creates an instance of OriginalFilenameFileOutputPathFormatter.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public KeepFilenameFormatter(string outputDirectory)
        {
            if (outputDirectory.Equals(""))
            {
                outputDirectory = ".";
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

        /// <summary>
        ///     Creates an instance of OriginalFilenameFileOutputPathFormatter.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        public static KeepFilenameFormatter Create(string outputDirectory)
        {
            return new KeepFilenameFormatter(outputDirectory);
        }
    }
}