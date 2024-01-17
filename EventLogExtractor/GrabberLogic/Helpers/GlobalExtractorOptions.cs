namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public static class GlobalExtractorOptions
    {
        public static string OutputEventViewerLogsDirectory = Path.Combine(
                 Directory.GetCurrentDirectory(),
                 "Logs",
                 $"{DateTime.Now.ToString("yyyy-MM-dd")}",
                 "EventViewerLogs");
        public static string AllEventsCsvName = "AllEvents.csv";
        public static string AllEventsCsvPath = Path.Combine(OutputEventViewerLogsDirectory, AllEventsCsvName);
    }
}
