using EventLogExtractor.ExtractorLogic.Helpers;
using System.Runtime.InteropServices;
using static EventLogExtractor.MainWindow;

namespace EventLogExtractor.ExtractorLogic
{
    public class EventViewerExtractor : IDisposable
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static System.ComponentModel.IContainer components = null;
        private SafeHandle _resource;
        public event ReceivedMessageEventHandler? OnStateChangedMessage;
        private int _currentLogGrabCounter = 0;
        private int _totalLogsToGrab = 0;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }

                if (_resource != null)
                {
                    _resource.Dispose();
                    _resource = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task GetLogsAsync(DateTime startTime, DateTime currentTime)
        {
            try
            {
                var loggerManager = new LoggerManager();
                WindowsHelper.PrepareOutputDirectory();

                var eventNamesAndProviders = EventViewerHelper.GetProviderNames().Distinct(new EventSourceComparer()).ToList();
                _totalLogsToGrab = eventNamesAndProviders.Count() + 5;

                await ExportStandardLogsAsync(startTime, currentTime, loggerManager);
                _logger.Info("start exporting low level logs from Event Viewer");

                await ExportCustomLogsAsync(eventNamesAndProviders, startTime, currentTime, loggerManager);
                _logger.Info("All logs exported");


                // remove duplicates
                WindowsHelper.ClearOutputDirectory();
                OnStateChangedMessage?.Invoke(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Compressing,
                    CurrentWorkingLogsName = "Compressing...",
                    PercentageCompleted = CalculatePercentageCompleted()
                });

                // archive output directory
                WindowsHelper.ZipFolder(
                    GlobalExtractorOptions.OutputEventViewerLogsDirectory,
                    $"{GlobalExtractorOptions.OutputEventViewerLogsDirectory}_{DateTime.Now.ToString(GlobalExtractorOptions.DateTimeFormat)}.zip");

                OnStateChangedMessage?.Invoke(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Done,
                    CurrentWorkingLogsName = "",
                    PercentageCompleted = 100
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception on getting logs");
                OnStateChangedMessage?.Invoke(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Exception,
                    CurrentWorkingLogsName = ex.ToString(),
                    PercentageCompleted = CalculatePercentageCompleted()
                });        
            }
        }

        private async Task ExportStandardLogsAsync(DateTime startTime, DateTime currentTime, LoggerManager loggerManager)
        {
            var standardLogs = new List<string> { "System", "Application", "Security" };
            foreach (var log in standardLogs)
            {
                await loggerManager.ExportEventsAsync(log, startTime, currentTime);

                OnStateChangedMessage?.Invoke(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Working,
                    CurrentWorkingLogsName = log,
                    PercentageCompleted = CalculatePercentageCompleted()
                });
            }
        }

        private async Task ExportCustomLogsAsync(IEnumerable<EventSource> eventSources, DateTime startTime, DateTime currentTime, LoggerManager loggerManager)
        {
            foreach (var source in eventSources)
            {
                await loggerManager.ExportCustomLogsAsync(source.LogName, source.ProviderName, startTime, currentTime);
                
                OnStateChangedMessage?.Invoke(new ExtractorState
                {
                    CurrentWorkingState = PossibleExtractorStates.Working,
                    CurrentWorkingLogsName = $"{source.LogName} provider name: {source.ProviderName}",
                    PercentageCompleted = CalculatePercentageCompleted()
                });

                _logger.Info($"Exporting name: {source.LogName} provider name: {source.ProviderName}");
            }
        }

        int CalculatePercentageCompleted()
        {
            _currentLogGrabCounter++;
            if (_totalLogsToGrab == 0)
                throw new ArgumentException("Total logs number cannot be null");

            return (int)((_currentLogGrabCounter / (double)_totalLogsToGrab) * 100.0);
        }
    }
}
