using System;
using Image.Output;

namespace Image
{
    /// <summary>
    ///     TaskExecutorOptions is a class containing various parameters for the <see cref="TaskExecutor" /> class.
    /// </summary>
    public class TaskExecutorOptions
    {
        private IFileOutputFormatter _fileOutputFormatter;

        /// <summary>
        ///     The file output path formatter. It cannot be null.
        ///     A implementation of <see cref="IFileOutputFormatter" />.
        /// </summary>
        public IFileOutputFormatter FileOutputFormatter
        {
            get => _fileOutputFormatter;
            set => _fileOutputFormatter = value ?? throw new ArgumentException("Output formatter cannot be null!");
        }

        /// <summary>
        ///     A boolean indicating if compression should be performed after cleaning the images.
        /// </summary>
        public bool EnableCompression { get; set; } = true;
    }
}