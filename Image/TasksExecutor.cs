using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Image
{
    public class TasksExecutor
    {
        public ILogger Logger = NullLogger.Instance;

        public static TasksExecutor Create()
        {
            return new TasksExecutor();
        }

        public void CleanImage(string fileName, string newFilename)
        {
            ICleaner cleaner = new Cleaner(fileName);
            cleaner.CleanImage(newFilename);
        }

        public void ParallelCleanImages(IEnumerable<string> fileNames, IOutputFormatter outputFormatter)
        {
            Logger.LogInformation("Starting parallel image cleaning.");
            var tasks = new List<Task>();
            foreach (var fileName in fileNames)
            {
                var task = new Task(() => { CleanImage(fileName, outputFormatter.FormatOutputPath(fileName)); });
                tasks.Add(task);
                task.Start();
            }

            var result = Task.WhenAll(tasks);
            result.Wait();

            var successTasks = tasks.Count(t => t.IsCompletedSuccessfully);
            var errorTasks = tasks.Count() - successTasks;
            Logger.LogInformation($"All tasks completed. Success: {successTasks}, Errors: {errorTasks}");
        }
    }
}