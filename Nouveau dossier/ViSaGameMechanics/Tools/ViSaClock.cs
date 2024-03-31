namespace StandardTools.Analysis
{
    public class ViSaClock : IViSaMyClock
    {
        public DateTime startTime { get; private set; }
        public ViSaClock()
        {
            Reset();
        }
        public TimeSpan GetCurrentTimeSpan()
        {
            TimeSpan elapsed;
            elapsed = DateTime.Now - startTime;
            return elapsed;
        }
        public string GetCurrentTimeAsString()
        {
            DateTime endTime;
            TimeSpan elapsed;

            endTime = DateTime.Now;
            elapsed = endTime - startTime;

            return elapsed.ToString();
        }
        public void Reset()
        {
            startTime = DateTime.Now;
        }
    }
}

