using CommandLine;
using Image;
using Microsoft.Extensions.Logging;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ConsoleInterface
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(RunOptions);
        }

        private static void RunOptions(Options options)
        {
            var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            TaskExecutor.Logger = loggerFactory.CreateLogger(nameof(TaskExecutor));
            FilesRetriever.Logger = loggerFactory.CreateLogger(nameof(FilesRetriever));
            OriginalFilenameOutputFormatter.Logger =
                loggerFactory.CreateLogger(nameof(OriginalFilenameOutputFormatter));

            var outputFormatter = OriginalFilenameOutputFormatter.Create(options.DestinationDirectory);
            var executor = TaskExecutor.Create(new TaskExecutorOptions
            {
                EnableCompression = options.CompressFiles is true,
                OutputFormatter = outputFormatter
            });
            var filesRetriever = FilesRetriever.Create();


            executor.ParallelCleanImages(filesRetriever.GetFilenamesFromPath(options.SourceDirectory));
        }

        public class Options
        {
            [Option('c', "compress", Required = false, HelpText = "Compress images after cleaning.", Default = true)]
            public bool? CompressFiles { get; set; }

            [Option('d', "dest", Required = false, HelpText = "The destination directory.", Default = "./cleaned")]
            public string DestinationDirectory { get; set; }

            [Value(0, MetaName = "source", HelpText = "The source directory.", Default = ".")]
            public string SourceDirectory { get; set; }
        }
    }
}