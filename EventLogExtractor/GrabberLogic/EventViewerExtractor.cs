
using EventLogExtractor.ExtractorLogic.Helpers;
using System.Runtime.InteropServices;
using static EventLogExtractor.MainWindow;

namespace EventLogExtractor.ExtractorLogic
{
    public class EventViewerExtractor : IDisposable
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static System.ComponentModel.IContainer components = null;
        private static SafeHandle resource;
        public event ReceivedMessageEventHandler OnStateChangedMessage;
        int currentLogGrabCounter = 0;
        int totalLogsToGrab = 0;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (resource != null) resource.Dispose();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void GetLogs(DateTime startTime, DateTime currentTime)
        {
            try
            {
                currentLogGrabCounter = 0;
                var loggerManager = new LoggerManager();
                var EventNamesAndProviders = EventViewerHelper.GetProviderNames().Distinct(new EventSourceComparer()).ToList();

                WindowsHelper.PrepareOutputDirectory();

                OnStateChangedMessage(new ExtractorState { CurrentWorkingState = PossibleExtractorStates.Working, CurrentWorkingLogsName = "Preparing", PercentageCompleted = 0 });


                //5 - current added state like: Preparing, System, App, Sec, Compressing
                totalLogsToGrab = EventNamesAndProviders.Count() + 5;


                OnStateChangedMessage(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Working,
                    CurrentWorkingLogsName = "System",
                    PercentageCompleted = CalculatePercentageCompleted()
                });
                loggerManager.ExportEvents("System", startTime, currentTime);

                OnStateChangedMessage(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Working,
                    CurrentWorkingLogsName = "Application",
                    PercentageCompleted = CalculatePercentageCompleted()
                });
                loggerManager.ExportEvents("Application", startTime, currentTime);

                OnStateChangedMessage(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Working,
                    CurrentWorkingLogsName = "Security",
                    PercentageCompleted = CalculatePercentageCompleted()
                });
                loggerManager.ExportEvents("Security", startTime, currentTime);


                _logger.Info("start exporting low level logs from Event Viewer");
                foreach (var log in EventNamesAndProviders)
                {
                    OnStateChangedMessage(new ExtractorState
                    {
                        CurrentWorkingState = PossibleExtractorStates.Working,
                        CurrentWorkingLogsName = $"{log.LogName} provider name: {log.ProviderName}",
                        PercentageCompleted = CalculatePercentageCompleted()
                    });

                    _logger.Info($"Exporting name: {log.LogName} provider name: {log.ProviderName}");
                    loggerManager.ExportCustomLogs(log.LogName, log.ProviderName, startTime, currentTime);
                }
                _logger.Info("All logs exported");


                // remove duplicates
                WindowsHelper.ClearOutputDirectory();
                OnStateChangedMessage(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Compressing,
                    CurrentWorkingLogsName = "Compressing...",
                    PercentageCompleted = CalculatePercentageCompleted()
                });

                // archive output directory
                WindowsHelper.ZipFolder(
                    GlobalExtractorOptions.OutputEventViewerLogsDirectory,
                    $"{GlobalExtractorOptions.OutputEventViewerLogsDirectory}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.zip");

                OnStateChangedMessage(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Done,
                    CurrentWorkingLogsName = "",
                    PercentageCompleted = 100
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception on getting logs");
            }
        }
        int CalculatePercentageCompleted()
        {
            currentLogGrabCounter++;
            if (totalLogsToGrab == 0)
                throw new ArgumentException("Total logs number cannot be null");

            return (int)((currentLogGrabCounter / (double)totalLogsToGrab) * 100.0);
        }

    }
}
