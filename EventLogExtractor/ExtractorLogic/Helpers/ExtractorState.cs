namespace EventLogExtractor.ExtractorLogic.Helpers
{
    public class ExtractorState
    {
        public string CurrentWorkingLogsName { get; set; }
        public PossibleExtractorStates CurrentWorkingState { get; set; }
        public double PercentageCompleted { get; set; } = 0;
    }
}
