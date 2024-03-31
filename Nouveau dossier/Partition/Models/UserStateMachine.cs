
namespace ViSa.Models;
public delegate Task<string> OnPushNote();
public delegate string OnxPushNote();
public class UserStateMachine //: IUserStateMachine
{
    public OnPushNote myDelegate { get; private set; }
    public OnxPushNote myxDelegate { get;  set; }
    public UserStateMachine()
    {
        myDelegate = pushNoteAfetNSeconds;
        myxDelegate = pushNoteAfetNSecondsx;
    }

    private string pushNoteAfetNSecondsx()
    {
        return "xxxyyy";
    }

    public async Task<string> pushNoteAfetNSeconds()
    {
        string msg;
        msg = "pushedNote";

        Task<string> t, u, v, x, y, z;
        t = XX();
        u = XX();
        v = XX();
        x = XX();
        y = XX();
        z = XX();

        t.Start();
        u.Start();
        v.Start();
        x.Start();
        y.Start();
        z.Start();
        Task.WhenAll(t, u, v, x, y, z).Wait();
        await t;
        await u;
        await v;
        await x;
        await y;
        await z;

        msg = t.Result;

        return msg;
    }
    private Task<string> XX()
    {
        Task<string> output;
        output = new Task<string>(WaitThenDoAsync);
        return output;
    }
    private string WaitThenDoAsync()
    {
        Task delay;
        delay = MyDelay();
        delay.Wait();

        return "myReturn";
    }
    private Task MyDelay()
    {
        return Task.Delay(new TimeSpan(0, 0, 1));
    }
}
