using CommandLine;

namespace ConsoleInterface
{
    /// <summary>
    ///     ProgramOptions is a class defining command line options supported by this program.
    /// </summary>
    public class ProgramOptions
    {
        /// <summary>
        ///     CompressFiles indicates whether files should be compressed after being cleaned.
        /// </summary>
        [Option('c', "compress", Required = false, HelpText = "Compress images after cleaning.", Default = true)]
        public bool? CompressFiles { get; set; }

        /// <summary>
        ///     DestinationDirectory represents the destination directory for the cleaned images.
        /// </summary>
        [Option('d', "destination", Required = false,
            HelpText = "The destination directory for the cleaned images.",
            Default = "./cleaned")]
        public string DestinationDirectory { get; set; }

        /// <summary>
        ///     DestinationDirectory represents the destination directory for the cleaned images.
        /// </summary>
        [Option('l', "log-level", Required = false,
            HelpText =
                "The logging level of the program. Available log levels are: None,Trace,Debug,Information,Warning,Error,Critical.",
            Default = "Information")]
        public string LogLevel { get; set; }

        /// <summary>
        ///     SourceDirectory represents the source directory of images.
        /// </summary>
        [Value(0, MetaName = "source", HelpText = "The source directory of images.", Default = ".")]
        public string SourceDirectory { get; set; }
    }
}