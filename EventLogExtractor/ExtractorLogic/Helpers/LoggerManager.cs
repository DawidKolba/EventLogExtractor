using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public class LoggerManager
    {
        private ILogger logger;
        private string OutputEventViewerLogsDirectory = GlobalExtractorOptions.OutputEventViewerLogsDirectory;

        public LoggerManager()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog();
                })
                .BuildServiceProvider();

            logger = serviceProvider.GetRequiredService<ILogger<LoggerManager>>();
        }

        public async Task ExportEventsAsync(string logName, DateTime startTime, DateTime endTime)
        {
            try
            {
                EventLog eventLog = new EventLog(logName);
                var events = eventLog.Entries;


                var fileName = Path.Combine(
                    OutputEventViewerLogsDirectory,
                    $"__{logName}-{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.log");


                if (!Directory.Exists(OutputEventViewerLogsDirectory))
                    Directory.CreateDirectory(OutputEventViewerLogsDirectory);

                using (var file = System.IO.File.CreateText(fileName))
                {
                    foreach (EventLogEntry entry in events)
                    {
                        if (entry.TimeGenerated >= startTime && entry.TimeGenerated <= endTime)
                        {
                            DateTime time = entry.TimeGenerated == null ? new DateTime(0000, 00, 00) : (DateTime)entry.TimeGenerated;
                            file.WriteLine($"Time: {time.ToString("yyyy-MM-dd HH:mm:ss:fff")}");
                            file.WriteLine($"Log name: {logName}");
                            file.WriteLine($"Source: {entry.Source}");
                            file.WriteLine($"Instance ID: ;{entry.InstanceId};");
                            file.WriteLine($"Level: {entry.EntryType}");
                            file.WriteLine($"Message: {entry.Message}");
                            file.WriteLine(new string('-', 50));

                            var builder = new StringBuilder();
                            builder.Append($"Time: ;{time.ToString("yyyy-MM-dd HH:mm:ss:fff")};");
                            builder.Append($"Log name: ;{logName};");
                            builder.Append($"Source: ;{entry.Source};");
                            builder.Append($"Instance ID: ;{entry.InstanceId};");
                            builder.Append($"Level: ;{entry.EntryType};");
                            builder.Append($"Message: ;{entry.Message};");

                            WindowsHelper.AddEventToCsvFile(builder.ToString());
                        }
                    }
                }

                logger.LogInformation($"Exported {logName} events");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on exporting standard logs");
            }
        }

        public async Task ExportCustomLogsAsync(string logName, string providerName, DateTime startTime, DateTime endTime)
        {
            try
            {
                string queryString = string.Format(
                   $"*[System[Provider[@Name='{providerName}'] " +
                   $"and TimeCreated[timediff(@SystemTime) <= {(Int64)(DateTime.Now - startTime).TotalMilliseconds} " +
                   $"and timediff(@SystemTime) >= {(Int64)(DateTime.Now - endTime).TotalMilliseconds} " +
                   $"]]]"
                   );
                logger.LogInformation($"Generated query: {queryString}");

                EventLogQuery query = new EventLogQuery(logName, PathType.LogName, queryString);
                var reader = new EventLogReader(query);
                var fileName = Path.Combine(OutputEventViewerLogsDirectory, $"{logName}-{providerName}-{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.log");

                if (!Directory.Exists(OutputEventViewerLogsDirectory))
                    Directory.CreateDirectory(OutputEventViewerLogsDirectory);

                EventRecord eventRecord;
                while ((eventRecord = await Task.Run(() => reader.ReadEvent())) != null)
                {
                    if (eventRecord != null)
                    {
                        using (var file = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true)))
                        {
                            DateTime time = eventRecord.TimeCreated == null ? new DateTime(0000, 00, 00) : (DateTime)eventRecord.TimeCreated;
                            file.WriteLine($"Time: {time.ToString("yyyy-MM-dd HH:mm:ss:fff")}");
                            file.WriteLine($"Source: {eventRecord.ProviderName}");
                            file.WriteLine($"Event ID: {eventRecord.Id}");
                            file.WriteLine($"Level: {eventRecord.LevelDisplayName}");
                            file.WriteLine($"Message: {eventRecord.FormatDescription()}");
                            file.WriteLine(new string('-', 50));

                            var builder = new StringBuilder();
                            builder.Append($"Time: ;{time.ToString("yyyy-MM-dd HH:mm:ss:fff")};");
                            builder.Append($"Source: ;{eventRecord.ProviderName};");
                            builder.Append($"Event ID: ;{eventRecord.Id};");
                            builder.Append($"Level: ;{eventRecord.LevelDisplayName};");
                            builder.Append($"Message: ;{eventRecord.FormatDescription()};");

                            WindowsHelper.AddEventToCsvFile(builder.ToString());
                        }
                    }
                }

                logger.LogInformation($"Exported {logName} {providerName} events");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on exporting custom logs");
            }
        }
    }
}
