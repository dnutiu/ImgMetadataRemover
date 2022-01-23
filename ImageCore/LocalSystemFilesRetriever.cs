using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    /// <summary>
    ///     LocalSystemFilesRetriever reads files from the provided directory on the local system.
    /// </summary>
    public class LocalSystemFilesRetriever : IFilesRetriever
    {
        public static ILogger Logger = NullLogger.Instance;

        private LocalSystemFilesRetriever()
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

        public static LocalSystemFilesRetriever Create()
        {
            return new LocalSystemFilesRetriever();
        }
    }
}