using System;

namespace Image
{
    public class TaskExecutorOptions
    {
        private IOutputFormatter _outputFormatter;

        public IOutputFormatter OutputFormatter
        {
            get => _outputFormatter;
            set => _outputFormatter = value ?? throw new ArgumentException("Output formatter cannot be null!");
        }

        public bool EnableCompression { get; set; } = true;
    }
}