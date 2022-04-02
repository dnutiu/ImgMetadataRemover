using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleInterface
{
    /// <summary>
    ///     LocalFileBrowser reads files from the provided directory on the local system.
    /// </summary>
    public class LocalFileBrowser : IFileBrowser
    {
        public static ILogger Logger = NullLogger.Instance;

        private LocalFileBrowser()
        {
        }

        /// <summary>
        ///     Give a directory path it returns all the filenames.
        /// </summary>
        /// <param name="directoryPath">An absolute path pointing to a directory.</param>
        /// <returns>A list of file names found in the directory.</returns>
        public IEnumerable<string> GetFilenamesFromPath(string directoryPath)
        {
            Logger.LogInformation($"Getting files from {directoryPath}.");
            return Directory.GetFiles(directoryPath, "*.*");
        }

        public static LocalFileBrowser Create()
        {
            return new LocalFileBrowser();
        }
    }
}