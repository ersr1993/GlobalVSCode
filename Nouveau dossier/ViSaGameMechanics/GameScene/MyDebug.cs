namespace StandardTools.Analysis;

public class MyDebug : IDebug
{
    public void Log(string message)
    {
        Console.WriteLine(message,ConsoleColor.Yellow);
    }
}

