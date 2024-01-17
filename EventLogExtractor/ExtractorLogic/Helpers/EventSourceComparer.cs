namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public class EventSourceComparer : IEqualityComparer<EventSource>
    {
        public bool Equals(EventSource x, EventSource y)
        {
            if (x == null || y == null)
                return false;

            return x.LogName == y.LogName && x.ProviderName == y.ProviderName;
        }

        public int GetHashCode(EventSource obj)
        {
            if (obj == null)
                return 0;

            return obj.LogName.GetHashCode() ^ obj.ProviderName.GetHashCode();
        }
    }

}
