﻿using CommandLine;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ConsoleInterface
{
    public static class Program
    {
        private static ILoggerFactory _loggerFactory;
        public static ILogger Logger = NullLogger.Instance;

        /// <summary>
        ///     The console interface for the project and the main entrypoint.
        /// </summary>
        /// <param name="args">Command line provided args.</param>
        // ReSharper disable once UnusedMember.Local
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ProgramOptions>(args).WithParsed(RunOptions);
        }

        /// <summary>
        ///     RunOptions will be called after the command-line arguments were successfully parsed.
        /// </summary>
        private static void RunOptions(ProgramOptions options)
        {
            SetupLogging(options.LogLevel);
            var outputFormatter = DirectoryOutputSink.Create(options.DestinationDirectory);
            var executor = TaskExecutor.Create(new TaskExecutorOptions
            {
                EnableCompression = options.CompressFiles is true,
                OutputSink = outputFormatter
            });
            var filesRetriever = LocalFileBrowser.Create();


            executor.ParallelCleanImages(filesRetriever.GetFilenamesFromPath(options.SourceDirectory));
        }

        public static void SetupLogging(string logLevel)
        {
            _loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddConsole();
                var logLevelToBeSet = LogLevel.Information;
                switch (logLevel.ToLower())
                {
                    case "trace":
                    case "t":
                    {
                        logLevelToBeSet = LogLevel.Trace;
                        break;
                    }
                    case "debug":
                    case "d":
                    {
                        logLevelToBeSet = LogLevel.Debug;
                        break;
                    }
                    case "information":
                    case "i":
                    case "info":
                    {
                        logLevelToBeSet = LogLevel.Information;
                        break;
                    }
                    case "warning":
                    case "warn":
                    case "w":
                    {
                        logLevelToBeSet = LogLevel.Warning;
                        break;
                    }
                    case "error":
                    case "err":
                    case "e":
                    {
                        logLevelToBeSet = LogLevel.Error;
                        break;
                    }
                    case "critical":
                    case "crt":
                    case "c":
                    {
                        logLevelToBeSet = LogLevel.Critical;
                        break;
                    }
                    case "none":
                    case "n":
                    {
                        logLevelToBeSet = LogLevel.None;
                        break;
                    }
                }

                b.SetMinimumLevel(logLevelToBeSet);
            });
            Logger = _loggerFactory.CreateLogger(nameof(Program));
            // Tasks
            TaskExecutor.Logger = _loggerFactory.CreateLogger(nameof(TaskExecutor));
            // File Retriever
            LocalFileBrowser.Logger = _loggerFactory.CreateLogger(nameof(LocalFileBrowser));
            // FileName formatter
            DirectoryOutputSink.Logger =
                _loggerFactory.CreateLogger(nameof(DirectoryOutputSink));
            FileSystemHelpers.Logger = _loggerFactory.CreateLogger(nameof(FileSystemHelpers));
            Logger.LogTrace("SetupLogging - exit");
        }
    }
}