﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image.Core;
using ImageMagick;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleInterface
{
    /// <summary>
    ///     TaskExecutor is a helper class for executing tasks in parallel.
    /// </summary>
    public class TaskExecutor
    {
        public static ILogger Logger = NullLogger.Instance;
        private readonly TaskExecutorOptions _options;

        /// <summary>
        ///     Creates a new instance of TaskExecutor.
        /// </summary>
        /// <param name="options">The TaskExecutor options.</param>
        /// <exception cref="ArgumentException">Raised when the options are null.</exception>
        private TaskExecutor(TaskExecutorOptions options)
        {
            _options = options ?? throw new ArgumentException("Options cannot be null!");
        }

        /// <summary>
        ///     Creates a new instance of TaskExecutor by calling the private constructor.
        /// </summary>
        /// <param name="options">The TaskExecutor options.</param>
        public static TaskExecutor Create(TaskExecutorOptions options)
        {
            return new TaskExecutor(options);
        }

        /// <summary>
        ///     Cleans an image. Errors are silenced by default.
        /// </summary>
        /// <param name="fileName">The file name of the image to be cleaned.</param>
        /// <param name="newFilename">The new file name of the cleaned image.</param>
        /// <returns>True of the image was cleaned, false otherwise.</returns>
        public bool CleanImage(string fileName, string newFilename)
        {
            try
            {
                ICompressor compressor = NullCompressor.Instance;
                var imageMagick = new MagickImage(fileName);
                if (_options.EnableCompression) compressor = LosslessCompressor.Instance;

                Logger.LogDebug(
                    $"Cleaning {fileName}, compression {_options.EnableCompression}, outputFormatter {nameof(_options.FileOutputFormatter)}.");
                IMetadataRemover metadataRemover = new ExifMetadataRemoverAndCompressor(imageMagick, compressor);
                metadataRemover.CleanImage(newFilename);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Cleans images in parallel using the built in Task Parallel Library.
        /// </summary>
        /// <param name="fileNames">An enumerable of file names.</param>
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
                    CleanImage(fileName, _options.FileOutputFormatter.GetOutputPath(fileName)));
                tasks.Add(task);
                task.Start();
            }

            var result = Task.WhenAll(tasks);
            result.Wait();

            var successTasks = tasks.Count(t => t.IsCompletedSuccessfully && t.Result);
            var errorTasks = tasks.Count - successTasks;
            Logger.LogInformation($"All tasks completed. Success: {successTasks}, Errors: {errorTasks}");
        }
    }
}