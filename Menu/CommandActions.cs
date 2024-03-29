using System.Collections.Generic;
using System;
using System.Linq;

namespace VsConsole;
internal class CommandActions
{
    internal Action LoadedDelegate { get; private set; }
    internal Dictionary<string, Action> _commandList { get; set; }

    public int SelectedFunctionId { get; private set; }
    public CommonActions _CommonActions { get; private set; }

    public CommandActions()
    {
        _commandList = new Dictionary<string, Action>();
        _CommonActions = new CommonActions();
    }
    public void _________() { AddCommand("___ ___ ___", () => { }); }
    public void AddCommand(string name, Action delegatedAction)
    {
        string cmdName;
        cmdName = $"{_commandList.Count}- {name}";
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
        _commandList.Add($"{_commandList.Count} - {methodName}", FinalAction);
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
        _commandList.Add($"{_commandList.Count} - {methodName}", FinalAction);
    }

    public void AddCommand(string methodName, string paramName, Action<string> delegatedMethod)
    {
        Action FinalAction;
        string msg;
        msg = $"veuillez entrer {paramName}: ";
        FinalAction = () => _CommonActions.AskUserValue.Invoke(msg);
        _commandList.Add($"{_commandList.Count} - {methodName}", FinalAction);
    }
    public void AddCommand(string methodName, Action<string> delegatedMethod, string inputValue)
    {
        Action FinalAction;
        FinalAction = () => delegatedMethod(inputValue);
        _commandList.Add($"{_commandList.Count.ToString()} - {methodName}", FinalAction);
    }
    public void AddCommand(Action<string> delegatedMethod, string inputValue)
    {
        Action FinalAction;
        string name;
        name = delegatedMethod.Method.Name;
        FinalAction = () => delegatedMethod(inputValue);
        _commandList.Add($"{_commandList.Count} - {name}", FinalAction);
    }

    internal void LoadSelectedDelegateSelectedFunc(int lineId)
    {
        LoadedDelegate = (_commandList.Values.Count > 0) ? _commandList.Values.ElementAt(lineId) : null;
    }

    internal void SelectFunction(int selLine)
    {
        SelectedFunctionId = selLine;

        LoadSelectedDelegateSelectedFunc(selLine);
    }
}
