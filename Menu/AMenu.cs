using System.Collections.Generic;
using System;
using System.Linq;
using System.Data;
using VsConsole.Logic.PageConsole;
using VsConsole.Logic;
using StandardTools;
using StandardTools.Reflexions;
using StandardTools.Utilities;
using System.Diagnostics;
using VsConsole.Menu;

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
        _navigator = new NavigationLogic(_actions);
        _dtU = new DataTableUtilities(diggingInterface);
    }

    protected abstract void SetupCommands();
    public void AddShortCommand(ConsoleKey shortCut, Action delegatedAction)
    {
        _navigator.AddKeyFuncAssociation(shortCut, delegatedAction);
        AddCommand(shortCut + delegatedAction.Method.Name, delegatedAction);
    }
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
            AddFooterMessage(testOutput);
        });
    }
    public virtual void Open()
    {
        _actions.ClearActions();
        //this._actions._commandList.Clear();
        SetupCommands();

        while (_navigator.StayInLoop())
        {
            RefreshSelection(SelectedFunctionId);
            DisplayPage();
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
        AddFooterMessage(errorMessage, ConsoleColor.Red);
#if DEBUG
        string fullInfo;
        fullInfo = $" \n - StackTrace : \n  " +
                    $"{e.StackTrace}";
        MyConsole.Say(errorMessage, ConsoleColor.Red);
        MyConsole.SayAndWait(fullInfo, ConsoleColor.DarkYellow);
#endif
    }

    public void _________()
    {
        _actions._________();
    }
    public void AddCommand(string name, Action delegatedAction)
    {
        _actions.AddCommand(name, delegatedAction);
    }
    public void AddCommand(string paramName, Action<string> delegatedMethod)
    {
        _actions.AddCommand(paramName, delegatedMethod);
    }
    public void AddCommand(string paramName, Action<int> delegatedMethod)
    {
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
    }
    public void AddCommand(Action<string> delegatedMethod, string inputValue)
    {
        _actions.AddCommand(delegatedMethod, inputValue);
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
            dt = _dtU.ToDataTable_Interface(list);
            AddFooterMessage(dt);
        }
        else
        {
            AddFooterMessage("Empty List");
        }
    }
    public void AddFooterMessageO<T>(T singleObject) where T : class
    {
        List<T> asList;
        asList = new List<T>()
        {
            singleObject
        };
        AddFooterMessage<T>(asList);
    }
    public void AddFooterMessage(IEnumerable<string> messages, ConsoleColor color = _FOOTER_DEFAULTCOLOR, bool isStringList = false, int lgth = 15)
    {
        string fullLine;

        int margin;

        fullLine = string.Empty;
        foreach (string message in messages)
        {
            if (isStringList)
            {
                lgth = message.Length;
            }
            margin = lgth + 5;
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
            //fullLine += $"{shortMsg,m}";
            fullLine += shortMsg.PadRight(margin);
            if (isStringList)
            {
                fullLine += "\n";
            }
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
        for (int i = 0; i < _actions._commandList.Count; i++)
        {
            string funcTitle;

            funcTitle = _actions._commandList.ElementAt(i).Key;
            //funcTitle = _actions.title(i);
            _body += $"{GetTitleWithStyle(lineId, funcTitle, isSelectedLine: IsSelectedId(lineId))} \n";

            lineId++;
        }
        //for (int i = 0; i < _actions._commandList.Count; i++)
        //foreach (string t in _actions._commandList.Keys.Where((x) => !x.StartsWith("_")))
        //{
        //    string funcTitle;

        //    //funcTitle = _actions._commandList.ElementAt(i).Key;
        //    funcTitle = t;
        //    //funcTitle = _actions.title(i);
        //    _body += $"{GetTitleWithStyle(lineId, funcTitle, isSelectedLine: IsSelectedId(lineId))} \n";

        //    lineId++;
        //}
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
        return _actions._commandList.Count;
    }

    protected virtual void AddHelloWorld()
    {
        AddCommand(HelloWorld);
    }
    private void HelloWorld()
    {
        AddFooterMessage("Hello World !", ConsoleColor.Green);
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
            userWill = AskUserint(msg);
        };

        int lastSliceCount;
        lastSliceCount = deckCount;
        if (userWill == resCountTranche)
        {
            lastSliceCount = count - resCountTranche * deckCount;
        }

        res = items.ToList().GetRange(userWill * deckCount, lastSliceCount);
        AddFooterMessage($"Tranche demandée :{userWill * deckCount} : {userWill * deckCount + lastSliceCount - 1}:", ConsoleColor.DarkBlue);
        AddFooterMessage(res);
    }

    protected void Ok()
    {
        //string msg;
        //msg = msg ?? string.Empty;
        AddFooterMessage("Ok");
    }
}
