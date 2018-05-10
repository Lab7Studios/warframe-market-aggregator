using System;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Json;

namespace WarframeMarketAggregator.App_Start
{
    public class SerilogConfig
    {
        public static void Configure(IOptions<ServiceSettings> serviceSettings)
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext();

            var rollingFileSinkConfig = serviceSettings.Value.GlobalLogger.RollingFileSink;
            if (rollingFileSinkConfig.Enabled)
            {
                loggerConfig.WriteTo.RollingFile(
                    new JsonFormatter(),
                    String.Format(rollingFileSinkConfig.LogFilePath + rollingFileSinkConfig.LogFileNameTemplate, "{Date}"),
                    retainedFileCountLimit: rollingFileSinkConfig.RetainedFileCountLimit
                );
            }

            loggerConfig.WriteTo.ColoredConsole();

            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
