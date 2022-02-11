using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image.Files
{
    public static class FileSystemHelpers
    {
        public static ILogger Logger = NullLogger.Instance;
        
        /// <summary>
        /// Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="destinationDirectory">The destination directory.</param>
        public static void CreateDestinationDirectory(string destinationDirectory)
        {
#if NETSTANDARD2_1
    Console.WriteLine("");
#elif  NETSTANDARD2_0
    Console.WriteLine("");
#elif NET5_0
            if (Directory.Exists(destinationDirectory)) return;
            //Logger.LogWarning("Output directory does not exist. Creating.");
            Directory.CreateDirectory(destinationDirectory);
#endif


        }
    }
}