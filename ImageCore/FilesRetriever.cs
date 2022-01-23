using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    public class FilesRetriever
    {
        public static ILogger Logger = NullLogger.Instance;

        private FilesRetriever()
        {
        }

        public static FilesRetriever Create()
        {
            return new FilesRetriever();
        }

        public IEnumerable<string> GetFilenamesFromPath(string path)
        {
            Logger.LogInformation($"Getting files from {path}.");
            return Directory.GetFiles(path, "*.*");
        }
    }
}