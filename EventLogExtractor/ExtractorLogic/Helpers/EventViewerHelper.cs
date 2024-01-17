using System.Diagnostics.Eventing.Reader;

namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public static class EventViewerHelper
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public static List <EventSource> GetProviderNames()
        {
            HashSet<string> providerNames = new HashSet<string>();
            var output = new List<EventSource>();
            foreach (var logName in GetAllLogNames())
            {
                try
                {
                    EventLogQuery query = new EventLogQuery(logName, PathType.LogName, "*");
                    using (EventLogReader reader = new EventLogReader(query))
                    {
                        for (EventRecord eventRecord = reader.ReadEvent(); eventRecord != null; eventRecord = reader.ReadEvent())
                        {
                            output.Add(new EventSource() { LogName = logName, ProviderName = eventRecord.ProviderName });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }

            return output;
        }

        public static string[] GetAllLogNames()
        {
            using (EventLogSession session = new EventLogSession())
            {
                return session.GetLogNames().ToArray();
            }
        }
    }
}
