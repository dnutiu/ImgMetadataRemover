using System;
using Image.Files;

namespace Image.Tasks
{
    /// <summary>
    ///     TaskExecutorOptions is a class containing various parameters for the <see cref="TaskExecutor" /> class.
    /// </summary>
    public class TaskExecutorOptions
    {
        private IOutputSink _outputSink;

        /// <summary>
        ///     The file output path formatter. It cannot be null.
        ///     A implementation of <see cref="IOutputSink" />.
        /// </summary>
        public IOutputSink OutputSink
        {
            get => _outputSink;
            set => _outputSink = value ?? throw new ArgumentException("OutputSink cannot be null!");
        }

        /// <summary>
        ///     A boolean indicating if compression should be performed after cleaning the images.
        /// </summary>
        public bool EnableCompression { get; set; } = true;
    }
}