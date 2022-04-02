using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleInterface
{
    public static class FileSystemHelpers
    {
        public static ILogger Logger = NullLogger.Instance;

        /// <summary>
        ///     Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="directoryPath">The destination directory's path.</param>
        public static void CreateDestinationDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                return;
            }

            Logger.LogWarning("Output directory does not exist. Creating.");
            Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        ///     CheckIfFileExists checks if file exists.
        /// </summary>
        /// <param name="filePath">The path of the file to be checked.</param>
        /// <returns>Returns true if file exists, False otherwise.</returns>
        public static bool CheckIfFileExists(string filePath)
        {
            var result = File.Exists(filePath);
            Logger.LogDebug(result
                ? $"CheckIfFileExists - {filePath} exists."
                : $"CheckIfFileExists - {filePath} doesn't exists.");
            return result;
        }
    }
}