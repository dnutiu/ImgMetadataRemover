using Image;
using Microsoft.Extensions.Logging;

namespace ConsoleInterface
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var outputFormatter = OriginalFilenameOutputFormatter.Create(@"C:\Users\nutiu\Downloads\Photos-001\clean");
            var executor = TasksExecutor.Create();
            executor.Logger = loggerFactory.CreateLogger("Executor");
            executor.ParallelCleanImages(new[]
            {
                @"C:\Users\nutiu\Downloads\Photos-001\IMG_0138.HEIC",
                @"C:\Users\nutiu\Downloads\Photos-001\IMG_0137.HEIC",
                @"C:\Users\nutiu\Downloads\Photos-001\IMG_0140.HEIC",
                @"C:\Users\nutiu\Downloads\Photos-001\12382975864_09e6e069e7_o.jpg"
            }, outputFormatter);
        }
    }
}