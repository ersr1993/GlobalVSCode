using StandardTools.Analysis;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ViSaGameMechanics;

public class EventEmitter
{
    public delegate string D_TickTack();
    public D_TickTack tickTack;
    private IViSaMyClock _clock { get; init; }

    public EventEmitter(IViSaMyClock myClock)
    {
        _clock = myClock;
        _clock.Reset();
        tickTack += Tick;
    }
    public string Tick()
    {
        return "tick";
    }
    public void EmitterConcrete()
    {
        TimeSpan timeSinceLastEvent;
        timeSinceLastEvent = _clock.GetCurrentTimeSpan();
        if (timeSinceLastEvent > TimeSpan.FromSeconds(1))
        {
            tickTack.Invoke();
            _clock.Reset();
        }
    }

}