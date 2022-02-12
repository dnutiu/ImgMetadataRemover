using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image.Files
{
    /// <summary>
    ///     LocalSystemFileBrowser reads files from the provided directory on the local system.
    /// </summary>
    public class LocalSystemFileBrowser : IFilesBrowser
    {
        public static ILogger Logger = NullLogger.Instance;

        private LocalSystemFileBrowser()
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

        public static LocalSystemFileBrowser Create()
        {
            return new LocalSystemFileBrowser();
        }
    }
}