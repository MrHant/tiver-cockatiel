using System;
using System.IO;
using NUnit.Framework;
using Serilog;
using Serilog.Formatting.Json;

namespace tiver_cockatiel.Logging
{
    public static class Logger
    {
        public static void Configure()
        {
            if (_configured)
            {
                return;
            }

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new TestDetailsEnricher())
                .WriteTo.Async(
                    a => a.File(
                        new JsonFormatter(),
                        Path.Combine(TestContext.CurrentContext.TestDirectory, $"./output/{DateTime.Now:yyyyMMdd_hhmmss}_log.txt")))
                .CreateLogger();
            _configured = true;
        }

        public static void Information(string logType, string message)
        {
            var log = Log.ForContext("LogType", logType);
            log.Information(message);
        }
        
        private static bool _configured;
    }
}