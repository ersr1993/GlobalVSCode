using StandardTools.Analysis;
using System.Diagnostics;

public class Metronom
{
    public IViSaMyClock _clock { get; private set; }
    public Action Tick { get; set; }
    public int TimeSignature { get; set; }
    private TimeSpan _nextTime = new TimeSpan(0, 0, 0, 1);
    private TimeSpan tickSpan = new TimeSpan(0, 0, 0, 0, 500);

    //private TimeSpan _nextTime = new TimeSpan(0, 0, 2);

    //private TimeSpan _expectedState = new TimeSpan(0, 0, 1);
    public Metronom(IViSaMyClock myClock)
    {
        _clock = myClock;
        this._clock.Reset();
    }
    // --- --- ---
    public void InvokesAction_WheenNeeded()
    { 
        // Called inside Update Loop 
        TimeSpan current;

        current = _clock.GetCurrentTimeSpan();
        if (_nextTime < current)
        {
            _nextTime += tickSpan;
            CallAction();
        }
    }

    private void CallAction()
    {
        Tick?.Invoke();
    }
}

