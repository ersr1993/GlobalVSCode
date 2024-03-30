using System.Collections.Generic;
using System;
using System.Linq;
using System.Data;
using VsConsole.Logic.PageConsole;
using VsConsole.Logic;
using StandardTools;
using StandardTools.Reflexions;
using StandardTools.Utilities;

namespace VsConsole;

public abstract class AMenu : APage, IMenu, IDisposable
{
    public int SelectedFunctionId { get { return _actions.SelectedFunctionId; } }
    internal CommandActions _actions { get; private set; }

    private NavigationLogic _navigator { get; set; }
    private IDataTableUtilities _dtU { get; set; }


    private const ConsoleColor _FOOTER_DEFAULTCOLOR = ConsoleColor.Green;

    public AMenu(string someTitle = "Menu")
    {
        InstanciateDependencies();
        _navigator.AddKeyFuncAssociation(ConsoleKey.C, ClearFooter);
        _title = someTitle;
        _footerItems = new List<(string, ConsoleColor?)>();
    }
    private void InstanciateDependencies()
    {
        DiggingObject diggingInterface;
        DiggingTypes diggingTypes;
        diggingTypes = new DiggingTypes();
        diggingInterface = new DiggingObject(diggingTypes);

        _actions = new CommandActions();
        _navigator = new NavigationLogic(this._actions);
        _dtU = new DataTableUtilities(diggingInterface);
    }

    protected abstract void SetupCommands();
    public void AddCommand(Action delegatedAction)
    {
        string commandLabel;
        commandLabel = delegatedAction.Method.Name;
        AddCommand(commandLabel, delegatedAction);
    }
    public void AddCommand(Func<string> TestFunc)
    {
        string commandLabel;
        commandLabel = TestFunc.Method.Name;
        AddCommand(commandLabel, () =>
        {
            string testOutput = TestFunc();
            this.AddFooterMessage(testOutput);
        });
    }
    public virtual void Open()
    {
        this._actions._commandList.Clear();
        SetupCommands();

        while (_navigator.StayInLoop())
        {
            RefreshSelection(SelectedFunctionId);
            this.DisplayPage();
            ExpectsKeyDown();
        }
        Dispose();
    }
    public virtual void Dispose() { }
    private void ExpectsKeyDown()
    {
        try
        {
            _navigator.AskUserKeyDown();
        }
        catch (Exception e)
        {
            DisplayErrorMessage(e);
        }
    }
    private void DisplayErrorMessage(Exception e)
    {
        string errorMessage;
        errorMessage = $"\n \n  *** *** *** ERROR : *** *** *** \n " +
                       $"-  {e.Message} \n\n ";
        this.AddFooterMessage(errorMessage, ConsoleColor.Red);
#if DEBUG
        string fullInfo;
        fullInfo = $" \n - StackTrace : \n  " +
                    $"{e.StackTrace}";
        MyConsole.Say(errorMessage, ConsoleColor.Red);
        MyConsole.SayAndWait(fullInfo, ConsoleColor.DarkYellow);
#endif
    }

    public void _________()
    { AddCommand("___ ___ ___", () => { }); }
    public void AddCommand(string name, Action delegatedAction)
    {
        string cmdName;
        cmdName = $"{_actions._commandList.Count}- {name}";
        _actions._commandList.Add(cmdName, delegatedAction);
        //_actions.AddCommand(name, delegatedAction);
    }
    public void AddCommand(string paramName, Action<string> delegatedMethod)
    {
        //Action FinalAction;
        //string methodName;

        //FinalAction = () =>
        //{
        //    string inputVar;
        //    inputVar = this.AskUserValue($"veuillez entrer {paramName}: ");
        //    delegatedMethod(inputVar);
        //};

        //methodName = delegatedMethod.Method.Name;
        //_actions._commandList.Add($"{_actions._commandList.Count} - {methodName}", FinalAction);
        _actions.AddCommand(paramName, delegatedMethod);
    }
    public void AddCommand(string paramName, Action<int> delegatedMethod)
    {
        //Action FinalAction;
        //string methodName;

        ////FinalAction = () =>
        ////{
        ////    int inputVar;
        ////    inputVar = this.AskUserint($"veuillez entrer {paramName} (int) : ");
        ////    delegatedMethod(inputVar);
        ////};
        //FinalAction = _actionsFactory.WithInputInt(paramName, delegatedMethod);

        //methodName = delegatedMethod.Method.Name;
        //_actions._commandList.Add($"{_actions._commandList.Count} - {methodName}", FinalAction);
        _actions.AddCommand(paramName, delegatedMethod);
    }

    public void AddCommand(string methodName, string paramName, Action<string> delegatedMethod)
    {
        //_actions.AddCommand(methodName, paramName,delegatedMethod);
        //Action FinalAction;
        //string msg;

        //FinalAction = _actionsFactory.WithInputString(paramName, delegatedMethod);
        //msg = $"{_actions._commandList.Count} - {methodName}";
        //_actions._commandList.Add(msg, FinalAction);
        _actions.AddCommand(methodName, paramName, delegatedMethod);
    }
    public void AddCommand(string methodName, Action<string> delegatedMethod, string inputValue)
    {
        _actions.AddCommand(methodName, delegatedMethod, inputValue);
        //Action FinalAction;
        //FinalAction = () => delegatedMethod(inputValue);
        //_actions._commandList.Add($"{_actions._commandList.Count.ToString()} - {methodName}", FinalAction);
    }
    public void AddCommand(Action<string> delegatedMethod, string inputValue)
    {
        _actions.AddCommand(delegatedMethod, inputValue);
        //Action FinalAction;
        //string name;
        //name = delegatedMethod.Method.Name;
        //FinalAction = () => delegatedMethod(inputValue);
        //_actions._commandList.Add($"{_actions._commandList.Count} - {name}", FinalAction);
    }


    public void AddFooterMessage(string message)
    {
        (string, ConsoleColor?) line;
        line = (message, _FOOTER_DEFAULTCOLOR);
        _footerItems?.Add(line);
    }
    public void AddFooterMessage(string footerItem, ConsoleColor color)
    {
        (string, ConsoleColor) line;
        line = (footerItem, color);
        _footerItems.Add(line);
    }
    public void AddFooterMessage<T>(IEnumerable<T> objects)
    {
        DataTable dt;
        List<T> list;
        list = objects.ToList();
        if (list.Any())
        {
            dt = _dtU.ToDataTable_Interface<T>(list);
            AddFooterMessage(dt);
        }
        else
        {
            AddFooterMessage("Empty List");
        }
    }
    public void AddFooterMessage(IEnumerable<string> messages, ConsoleColor color = _FOOTER_DEFAULTCOLOR, bool isStringList = false)
    {
        string fullLine;
        const int lgth = 15;
        const int margin = -(lgth + 5);

        fullLine = string.Empty;
        foreach (string message in messages)
        {
            string shortMsg;

            shortMsg = message.Substring(0, Math.Min(lgth, message.Length));
            if (shortMsg.Length == 0)
            {
                shortMsg += "¤";
            }
            else if (!(shortMsg.Length == message.Length))
            {
                shortMsg += "...";
            }

            fullLine += $"{shortMsg,margin}";
        }

        AddFooterMessage(fullLine, color);
    }
    public void AddFooterMessage(DataTable dt)
    {
        string stringDataTable;
        stringDataTable = ObjToString.Convert(dt);
        AddFooterMessage(stringDataTable);
    }

    public float AskUserFloat(string messageToDisplay)
    {
        return _actions._CommonActions.AskUserFloat.Invoke(messageToDisplay);
    }
    public int AskUserint(string messageToDisplay)
    {
        return _actions._CommonActions.AskUserInt.Invoke(messageToDisplay);
    }
    public string AskUserValue(string messageToDisplay)
    {
        string userValue;
        userValue = _actions._CommonActions.AskUserValue.Invoke(messageToDisplay);
        return userValue;
    }

    internal void RefreshSelection(int selLine)
    {
        int lineId;

        _body = string.Empty;

        _actions.SelectFunction(selLine);

        lineId = 0;
        for (int i = 0; i < _actions._commandList.Count(); i++)
        {
            string funcTitle = _actions._commandList.ElementAt(i).Key;

            _body += $"{GetTitleWithStyle(lineId, funcTitle, isSelectedLine: IsSelectedId(lineId))} \n";

            lineId++;
        }
    }
    private bool IsSelectedId(int lineId)
    {
        return lineId == SelectedFunctionId;
    }
    private string GetTitleWithStyle(int id, string lineOfText, bool isSelectedLine)
    {
        string outputString;

        if (isSelectedLine)
        {
            outputString = $"* {lineOfText.ToUpper()}  ***";
        }
        else
        {
            outputString = $"{lineOfText.ToLower()}";
        }

        return outputString;
    }
    public int CountCommands()
    {
        return this._actions._commandList.Count;
    }

    protected virtual void AddHelloWorld()
    {
        this.AddCommand(HelloWorld);
    }
    private void HelloWorld()
    {
        this.AddFooterMessage("Hello World !", ConsoleColor.Green);
    }
    protected virtual void DisplayListSubItems<T>(IEnumerable<T> items, int deckCount = 50)
    {
        IEnumerable<T> res;
        int userWill, count;
        string msg;
        decimal resTenths;
        int resCountTranche;

        count = items.Count();
        resTenths = count / deckCount;
        resCountTranche = (int)Math.Floor(resTenths);
        msg = $"Tranche de {deckCount} resultats 0 et {Math.Max(resCountTranche - 1, 0)}";
        userWill = int.MaxValue;
        while (userWill > resCountTranche - 1 && userWill != 0)
        {
            userWill = this.AskUserint(msg);
        };

        int lastSliceCount;
        lastSliceCount = deckCount;
        if (userWill == resCountTranche)
        {
            lastSliceCount = count - (resCountTranche * deckCount);
        }

        res = items.ToList<T>().GetRange(userWill * deckCount, lastSliceCount);
        this.AddFooterMessage($"Tranche demandée :{userWill * deckCount} : {userWill * deckCount + lastSliceCount - 1}:", ConsoleColor.DarkBlue);
        this.AddFooterMessage(res);
    }

}
