using CommandLine;
using Image.Files;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ConsoleInterface
{
    public static class Program
    {
        private static ILoggerFactory _loggerFactory;
        public static readonly ILogger Logger = NullLogger.Instance;
        
        /// <summary>
        ///     The console interface for the project and the main entrypoint.
        /// </summary>
        /// <param name="args">Command line provided args.</param>
        // ReSharper disable once UnusedMember.Local
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(RunOptions);
        }

        /// <summary>
        ///     RunOptions will be called after the command-line arguments were successfully parsed.
        /// </summary>
        private static void RunOptions(Options options)
        {
            _loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            TaskExecutor.Logger = _loggerFactory.CreateLogger(nameof(TaskExecutor));
            LocalSystemFilesRetriever.Logger = _loggerFactory.CreateLogger(nameof(LocalSystemFilesRetriever));
            KeepFilenameFormatter.Logger =
                _loggerFactory.CreateLogger(nameof(KeepFilenameFormatter));

            CreateDestinationDirectory(options.DestinationDirectory);
            var outputFormatter = KeepFilenameFormatter.Create(options.DestinationDirectory);
            var executor = TaskExecutor.Create(new TaskExecutorOptions
            {
                EnableCompression = options.CompressFiles is true,
                FileOutputFormatter = outputFormatter
            });
            var filesRetriever = LocalSystemFilesRetriever.Create();


            executor.ParallelCleanImages(filesRetriever.GetFilenamesFromPath(options.SourceDirectory));
        }
        
        /// <summary>
        /// Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="destinationDirectory">The destination directory.</param>
        private static void CreateDestinationDirectory(string destinationDirectory)
        {
            if (FileSystemHelpers.Logger == null)
            {
                FileSystemHelpers.Logger = _loggerFactory.CreateLogger(nameof(FileSystemHelpers));
            }
            FileSystemHelpers.CreateDestinationDirectory(destinationDirectory);
        }

        /// <summary>
        ///     Options is a class defining command line options supported by this program.
        /// </summary>
        public class Options
        {
            /// <summary>
            ///     CompressFiles indicates whether files should be compressed after being cleaned.
            /// </summary>
            [Option('c', "compress", Required = false, HelpText = "Compress images after cleaning.", Default = true)]
            public bool? CompressFiles { get; set; }

            /// <summary>
            ///     DestinationDirectory represents the destination directory for the cleaned images.
            /// </summary>
            [Option('d', "dest", Required = false, HelpText = "The destination directory for the cleaned images.",
                Default = "./cleaned")]
            public string DestinationDirectory { get; set; }

            /// <summary>
            ///     SourceDirectory represents the source directory of images.
            /// </summary>
            [Value(0, MetaName = "source", HelpText = "The source directory of images.", Default = ".")]
            public string SourceDirectory { get; set; }
        }
    }
}