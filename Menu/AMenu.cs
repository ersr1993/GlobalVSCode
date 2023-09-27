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
public abstract class AMenu : APage, IMenu
{
    public int SelectedFunctionId { get; set; }

    protected Dictionary<string, Action> _commandList;
    private NavigationLogic _navigator;
    private IDataTableUtilities _dtU;

    internal Action LoadedDelegate { get; private set; }

    private const ConsoleColor _FOOTER_DEFAULTCOLOR = ConsoleColor.Green;

    public AMenu(string someTitle = "Menu")
    {
        _commandList = new Dictionary<string, Action>();

        DiggingObject diggingInterface;
        DiggingTypes diggingTypes;
        diggingTypes = new DiggingTypes();
        diggingInterface = new DiggingObject(diggingTypes);
        _dtU = new DataTableUtilities(diggingInterface);
        _navigator = new NavigationLogic(this);
        _title = someTitle;
        _footerItems = new List<(string, ConsoleColor?)>();
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
        this._commandList.Clear();
        SetupCommands()
;

        while (_navigator.StayInLoop())
        {
            RefreshSelection(SelectedFunctionId);
            this.DisplayPage();
            _navigator.AskUserKeyDown();
        }
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
            inputVar = this.AskUserValue($"veuillez entrer {paramName}: ");
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
            inputVar = this.AskUserint($"veuillez entrer {paramName} (int) : ");
            delegatedMethod(inputVar);
        };

        methodName = delegatedMethod.Method.Name;
        _commandList.Add($"{_commandList.Count} - {methodName}", FinalAction);
    }

    public void AddCommand(string methodName, string paramName, Action<string> delegatedMethod)
    {
        Action FinalAction;

        FinalAction = () =>
        {
            string inputVar;
            inputVar = this.AskUserValue($"veuillez entrer {paramName}: ");
            delegatedMethod(inputVar);
        };
        _commandList.Add($"{_commandList.Count.ToString()} - {methodName}", FinalAction);
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
        dt = _dtU.ToDataTable_Interface<T>(list);
        AddFooterMessage(dt);
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
        string outputAsString;
        outputAsString = AskUserValue(messageToDisplay);
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
    public int AskUserint(string messageToDisplay)
    {
        string outputAsString;
        outputAsString = AskUserValue(messageToDisplay);
        try
        {
            return int.Parse(outputAsString);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Impossible de convertir la valeur en entier", ex);
        }
    }
    public string AskUserValue(string messageToDisplay)
    {
        string output;

        output = MyConsole.AskTypeLine(messageToDisplay);

        return output;
    }

    internal void RefreshSelection(int selLine)
    {
        int lineId;

        _body = string.Empty;
        SelectedFunctionId = selLine;
        lineId = 0;
        for (int i = 0; i < _commandList.Count(); i++)
        {
            string funcTitle = _commandList.ElementAt(i).Key;

            _body += $"{GetTitleWithStyle(lineId, funcTitle, IsSelectedId(lineId))} \n";
            if (IsSelectedId(lineId))
            {
                LoadedDelegate = _commandList.Values.ElementAt(lineId);
            }
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
        return this._commandList.Count;
    }

    protected virtual void AddHelloWorld()
    {
        this.AddCommand(HelloWorld);
    }
    private void HelloWorld()
    {
        this.AddFooterMessage("Hello World !", ConsoleColor.Green);
    }
    protected virtual void DisplayListSubItems<T>(IEnumerable<T> items, int deckCount =50)
    {
        IEnumerable<T> res;
        int userWill, count;
        string msg;
        decimal resTenths;
        int resCountTranche;

        count = items.Count();
        resTenths = count / deckCount;
        resCountTranche = (int)Math.Floor(resTenths);
        msg = $"Tranche de {deckCount} resultats 0 et {Math.Max(resCountTranche-1,0)}";
        userWill = int.MaxValue;
        while (userWill > resCountTranche-1&& userWill!=0)
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
        this.AddFooterMessage($"Tranche demandée :{userWill*deckCount} : {userWill*deckCount+lastSliceCount-1}:",ConsoleColor.DarkBlue);
        this.AddFooterMessage(res);
    }

}