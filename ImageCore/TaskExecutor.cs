using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    public class TaskExecutor
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly TaskExecutorOptions _options;

        private TaskExecutor(TaskExecutorOptions options)
        {
            _options = options ?? throw new ArgumentException("Options cannot be null!");
        }

        public static TaskExecutor Create(TaskExecutorOptions options)
        {
            return new TaskExecutor(options);
        }

        public bool CleanImage(string fileName, string newFilename)
        {
            try
            {
                ICompressor compressor = NullCompressor.Instance;
                var imageMagick = new MagickImage(fileName);
                if (_options.EnableCompression)
                {
                    compressor = LosslessCompressor.Instance;
                }

                Logger.LogDebug(
                    $"Cleaning {fileName}, compression {_options.EnableCompression}, outputFormatter {nameof(_options.OutputFormatter)}.");
                IMetadataRemover metadataRemover = new MetadataRemover(imageMagick, compressor);
                metadataRemover.CleanImage(newFilename);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
            }
        }

        public void ParallelCleanImages(IEnumerable<string> fileNames)
        {
            Logger.LogInformation("Starting parallel image cleaning.");
            var filenamesArray = fileNames as string[] ?? fileNames.ToArray();
            if (!filenamesArray.Any())
            {
                Logger.LogWarning("Empty fileNames, nothing to do.");
                return;
            }

            var tasks = new List<Task<bool>>();
            foreach (var fileName in filenamesArray)
            {
                var task = new Task<bool>(() =>
                    CleanImage(fileName, _options.OutputFormatter.FormatOutputPath(fileName)));
                tasks.Add(task);
                task.Start();
            }

            var result = Task.WhenAll(tasks);
            result.Wait();

            var successTasks = tasks.Count(t => t.IsCompletedSuccessfully && t.Result);
            var errorTasks = tasks.Count() - successTasks;
            Logger.LogInformation($"All tasks completed. Success: {successTasks}, Errors: {errorTasks}");
        }
    }
}