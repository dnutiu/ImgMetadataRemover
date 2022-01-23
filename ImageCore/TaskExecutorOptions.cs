using System;

namespace Image
{
    /// <summary>
    ///     TaskExecutorOptions is a class containing various parameters for the <see cref="TaskExecutor" /> class.
    /// </summary>
    public class TaskExecutorOptions
    {
        private IFileOutputPathFormatter _fileOutputPathFormatter;

        /// <summary>
        ///     The file output path formatter. It cannot be null.
        ///     A implementation of <see cref="IFileOutputPathFormatter" />.
        /// </summary>
        public IFileOutputPathFormatter FileOutputPathFormatter
        {
            get => _fileOutputPathFormatter;
            set => _fileOutputPathFormatter = value ?? throw new ArgumentException("Output formatter cannot be null!");
        }

        /// <summary>
        ///     A boolean indicating if compression should be performed after cleaning the images.
        /// </summary>
        public bool EnableCompression { get; set; } = true;
    }
}