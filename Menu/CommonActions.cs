using System;
using VsConsole.Logic;

namespace VsConsole;

internal class CommonActions
{
    internal Action WithInputInt(string paramName, Action<int> delegatedMethod)
    {
        Action FinalAction;
        string methodName;

        FinalAction = () =>
        {
            int inputVar;
            inputVar = AskUserInt.Invoke($"veuillez entrer {paramName} (int) : ");
            delegatedMethod(inputVar);
        };

        return FinalAction;
    }
    internal Action WithInputString(string paramName, Action<string> delegatedMethod)
    {
        Action FinalAction;
        FinalAction = () =>
        {
            string inputVar;
            inputVar = AskUserValue($"veuillez entrer {paramName}: ");
            delegatedMethod(inputVar);
        };
        return FinalAction;
    }

    public Func<string, float> AskUserFloat = askUserFloat;
    public Func<string, int> AskUserInt = askUserint;
    public Func<string, string> AskUserValue = askUserValue;

    private static float askUserFloat(string messageToDisplay)
    {
        string outputAsString;
        outputAsString = askUserValue(messageToDisplay);
        try
        {
            return float.Parse(outputAsString);
        }
        catch (FormatException ex)
        {
            string msg;
            msg = "Impossible de convertir la valeur en entier";
            throw new FormatException(msg, ex);
        }
    }
    private static int askUserint(string messageToDisplay)
    {
        string outputAsString;
        outputAsString = askUserValue(messageToDisplay);
        try
        {
            return int.Parse(outputAsString);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Impossible de convertir la valeur en entier", ex);
        }
    }
    private static string askUserValue(string messageToDisplay)
    {
        string output;

        output = MyConsole.AskTypeLine(messageToDisplay);

        return output;
    }

}
