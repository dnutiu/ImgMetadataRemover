using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    public class TaskExecutor
    {
        public ILogger Logger = NullLogger.Instance;

        public static TaskExecutor Create()
        {
            return new TaskExecutor();
        }

        public bool CleanImage(string fileName, string newFilename)
        {
            try
            {
                ICleaner cleaner = new Cleaner(fileName);
                cleaner.CleanImage(newFilename);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
            }
        }

        public void ParallelCleanImages(IEnumerable<string> fileNames, IOutputFormatter outputFormatter)
        {
            Logger.LogInformation("Starting parallel image cleaning.");
            var tasks = new List<Task<bool>>();
            foreach (var fileName in fileNames)
            {
                var task = new Task<bool>(() => CleanImage(fileName, outputFormatter.FormatOutputPath(fileName)));
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