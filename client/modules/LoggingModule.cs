using System;
using Microsoft.Extensions.Logging;
using Serilog;

namespace client.modules
{
    internal class LoggingModule
    {
        private static readonly string logFilePath = Environment.GetEnvironmentVariable("log_path");
        private static readonly ILoggerFactory _loggerFactory;

        static LoggingModule()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(logFilePath, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}").CreateLogger();

            _loggerFactory = LoggerFactory.Create(builder =>{builder.AddSerilog(dispose: true);});
        }

        public static ILogger<T> CreateLogger<T>() => _loggerFactory.CreateLogger<T>();
    }
}
