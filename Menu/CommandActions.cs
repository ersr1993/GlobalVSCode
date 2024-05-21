using System.Collections.Generic;
using System;
using System.Linq;

namespace VsConsole.Menu;
internal class CommandActions
{

    internal Action LoadedDelegate { get; private set; }
    internal Dictionary<string, Action> _commandList { get; set; }

    public int SelectedFunctionId { get; private set; }
    public CommonActions _CommonActions { get; private set; }
    private int cmdCounts { get; set; }
    private int sepsCount { get; set; }

    public CommandActions()
    {
        _commandList = new Dictionary<string, Action>();
        _CommonActions = new CommonActions();
    }
    public void _________()
    {
        AddCommand(SEPARATOR, NULL_ACTION);
    }
    internal Action NULL_ACTION = () => { };
    private string SEPARATOR { get; set; } = "___ ___ ___";
    public void AddCommand(string name, Action delegatedAction)
    {
        string cmdName;
        if (delegatedAction == NULL_ACTION)
        {
            cmdName = $"{SEPARATOR} {sepsCount}";
            sepsCount++;
        }
        else
        {
            cmdName = $"{cmdCounts}- {name}";
            cmdCounts++;
        }

        _commandList.Add(cmdName, delegatedAction);
    }

    public void AddCommand(string paramName, Action<string> delegatedMethod)
    {
        Action FinalAction;
        string methodName;
        FinalAction = () =>
        {
            string inputVar;
            inputVar = _CommonActions.AskUserValue.Invoke($"veuillez entrer {paramName}: ");
            delegatedMethod(inputVar);
        };

        methodName = delegatedMethod.Method.Name;
        AddCommand(methodName, FinalAction);
    }
    public void AddCommand(string paramName, Action<int> delegatedMethod)
    {
        Action FinalAction;
        string methodName;

        FinalAction = () =>
        {
            int inputVar;
            inputVar = _CommonActions.AskUserInt.Invoke($"veuillez entrer {paramName} (int) : ");
            delegatedMethod(inputVar);
        };

        methodName = delegatedMethod.Method.Name;
        AddCommand(methodName, FinalAction);
    }

    public void AddCommand(string methodName, string paramName, Action<string> delegatedMethod)
    {
        Action FinalAction;
        string msg;
        msg = $"veuillez entrer {paramName}: ";
        FinalAction = () => _CommonActions.AskUserValue.Invoke(msg);

        AddCommand(methodName, FinalAction);
    }
    public void AddCommand(string methodName, Action<string> delegatedMethod, string inputValue)
    {
        Action FinalAction;
        FinalAction = () => delegatedMethod(inputValue);
        AddCommand(methodName, FinalAction);
    }
    public void AddCommand(Action<string> delegatedMethod, string inputValue)
    {
        Action FinalAction;
        string name;
        name = delegatedMethod.Method.Name;
        FinalAction = () => delegatedMethod(inputValue);
        AddCommand(name, FinalAction);
    }

    internal void LoadSelectedDelegateSelectedFunc(int lineId)
    {
        LoadedDelegate = _commandList.Values.Count > 0
                        ? _commandList.Values.ElementAt(lineId)
                        : null;
    }
    internal string title(int i)
    {
        string k = _commandList.Keys.Where((x) => x.StartsWith(i.ToString()))
                    .First();
        return k;
    }
    internal void SelectFunction(int selLine)
    {
        SelectedFunctionId = selLine;
        LoadSelectedDelegateSelectedFunc(selLine);
    }
    internal bool IsNullSelected()
    {
        bool output;
        //output = _commandList.ElementAt(this.SelectedFunctionId).Key.StartsWith(SEPARATOR);
        output = _commandList.ElementAt(SelectedFunctionId).Value == NULL_ACTION;
        return output;
    }

    internal void ClearActions()
    {
        _commandList.Clear();
        cmdCounts = 0;
        sepsCount = 0;
    }

}
