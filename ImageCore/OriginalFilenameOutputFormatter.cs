using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    public class OriginalFilenameOutputFormatter : IOutputFormatter
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly string _rootDirectory;

        public OriginalFilenameOutputFormatter(string rootDirectory)
        {
            if (!Directory.Exists(rootDirectory))
            {
                Logger.LogWarning("Output directory does not exists. Creating.");
                Directory.CreateDirectory(rootDirectory);
            }

            _rootDirectory = rootDirectory;
        }

        public string FormatOutputPath(string filePath)
        {
            var fileName = Path.GetFileName(filePath)?.Split(".")[0];
            var path = Path.Join(_rootDirectory, $"{fileName}.jpg");
            return path;
        }

        public static OriginalFilenameOutputFormatter Create(string rootDirectory)
        {
            return new OriginalFilenameOutputFormatter(rootDirectory);
        }
    }
}