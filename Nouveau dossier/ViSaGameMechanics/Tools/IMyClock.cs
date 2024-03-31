namespace StandardTools.Analysis;

public interface IViSaMyClock
{
    DateTime startTime { get; }

    TimeSpan GetCurrentTimeSpan();
    string GetCurrentTimeAsString();
    void Reset();
}