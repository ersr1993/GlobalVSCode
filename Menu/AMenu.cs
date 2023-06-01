namespace VsConsole;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data;
using VsConsole.Logic.PageConsole;
using VsConsole.Logic;
using StandardTools;
using StandardTools.Reflexions;

public abstract class AMenu : APage, IMenu
{
    public int SelectedFunctionId { get; set; }

    protected Dictionary<string, Action> _commandList;
    private NavigationLogic _navigator;
    private DataTableUtilities _dtU;

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
    public void AddCommand(Action myDelegateFunction)
    {
        string commandLabel;
        commandLabel = myDelegateFunction.Method.Name;
        AddCommand(commandLabel, myDelegateFunction);
    }
    public void AddCommand(string name, Action myDelegateFunction)
    {
        string cmdName;
        cmdName = $"{GetIncrementalId()}{name}";
        _commandList.Add(cmdName, myDelegateFunction);
    }
    private string GetIncrementalId()
    {
        string id;
        id = $"{_commandList.Count.ToString()} - ";
        return id;
    }
    public void AddCommand(string nomParametre, Action<string> myDelegateFunction)
    {
        Action FinalAction;
        string name;

        FinalAction = () =>
        {
            string inputVar;
            inputVar = this.AskUserValue($"veuillez entrer {nomParametre}: ");
            myDelegateFunction(inputVar);
        };
        name = myDelegateFunction.Method.Name;
        _commandList.Add($"{_commandList.Count} - {name}", FinalAction);
    }

    public void AddCommand(string name, string nomParametre, Action<string> myDelegateFunction)
    {
        Action FinalAction;


        FinalAction = () =>
        {
            string inputVar;
            inputVar = this.AskUserValue($"veuillez entrer {nomParametre}: ");
            myDelegateFunction(inputVar);
        };
        _commandList.Add($"{_commandList.Count.ToString()} - {name}", FinalAction);
    }
    public void AddCommand(string name, Action<string> myDelegateFunction, string inputVar)
    {
        Action FinalAction;
        FinalAction = () => myDelegateFunction(inputVar);
        _commandList.Add($"{_commandList.Count.ToString()} - {name}", FinalAction);
    }
    public void AddCommand(Action<string> myDelegateFunction, string inputVar)
    {
        Action FinalAction;
        string name;
        name = myDelegateFunction.Method.Name;
        FinalAction = () => myDelegateFunction(inputVar);
        _commandList.Add($"{_commandList.Count} - {name}", FinalAction);
    }

    public virtual void Open()
    {
        this._commandList.Clear();
        SetupCommands();

        while (_navigator.StayInLoop())
        {
            RefreshSelection(SelectedFunctionId);
            this.DisplayPage();
            _navigator.AskUserKeyDown();
        }
    }
    public void AddFooterMessage(DataTable dt)
    {
        string stringDataTable;
        stringDataTable = ObjToString.Convert(dt);
        AddFooterMessage(stringDataTable);
    }
    public void AddFooterMessage(string footerItem, ConsoleColor color)
    {
        (string, ConsoleColor) line;
        line = (footerItem, color);
        _footerItems.Add(line);
    }
    public void AddFooterMessage(string message)
    {
        (string, ConsoleColor?) line;
        line = (message, _FOOTER_DEFAULTCOLOR);
        _footerItems?.Add(line);
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
    public void AddFooterMessage<T>(IEnumerable<T> objects)
        where T : class
    {
        DataTable dt;
        List<T> list;

        list = objects.ToList();
        dt = _dtU.ToDataTable_Interface<T>(list);

        AddFooterMessage(dt);
    }
    //public void AddFooterMessage<U, U>(IEnumerable<U> objects)
    //        where U : class
    //{
    //    DataTable dt;
    //    List<U> list;

    //    list = objects.ToList();
    //    dt = _dtU.ToDataTable_Concrete<U,U>(list);

    //    AddFooterMessage(dt);
    //}
    private void RefreshSelection(int selLine)
    {
        _body = string.Empty;
        SelectedFunctionId = selLine;
        int lineId = 0;

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
}