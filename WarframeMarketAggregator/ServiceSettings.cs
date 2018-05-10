namespace WarframeMarketAggregator
{
    public class ServiceSettings
    {
        public string WarframeMarketBaseUrl { get; set; }

        public GlobalLoggerSection GlobalLogger { get; set; }
    }

    public class GlobalLoggerSection
    {
        public RollingFileSinkSection RollingFileSink { get; set; }
    }

    public class RollingFileSinkSection
    {
        public bool Enabled { get; set; }

        public string LogFilePath { get; set; }

        public string LogFileNameTemplate { get; set; }

        public int RetainedFileCountLimit { get; set; }
    }
}
