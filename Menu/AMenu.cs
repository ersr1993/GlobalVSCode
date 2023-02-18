namespace VsConsole
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Data;
    using VsConsole.Logic.PageConsole;
    using VsConsole.Logic;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public abstract class AMenu : APage, IMenu
    {
        public int SelectedFunctionId { get; set; }

        protected Dictionary<string, Action> _commandList;
        protected NavigationLogic _navigator;
        protected Action? _loadedDelegate;


        public AMenu(string someTitle="Menu")
        {
            _commandList = new Dictionary<string, Action>();
            _navigator = new NavigationLogic(this);
            _title = someTitle;
        }

        public void AddCommand(Action myDelegateFunction)
        {
            string name;
            name = myDelegateFunction.Method.Name;

            AddCommand(name, myDelegateFunction);
        }
        public void AddCommand(string name, Action myDelegateFunction)
        {
            string cmdName;

            cmdName = $"{GetIncrementalId()}{name}";
            _commandList.Add(cmdName, myDelegateFunction);

            RefreshSelection(SelectedFunctionId);
        }
        private string GetIncrementalId()
        {
            string id;
            id = $"{_commandList.Count.ToString()} - ";
            return id;
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
            RefreshSelection(SelectedFunctionId);
        }
        public void AddCommand(string name, Action<string> myDelegateFunction, string inputVar)
        {
            Action FinalAction;
            FinalAction = () => myDelegateFunction(inputVar);
            _commandList.Add($"{_commandList.Count.ToString()} - {name}", FinalAction);
            RefreshSelection(SelectedFunctionId);
        }

        public void Open()
        {
            while (_navigator.StayInLoop())
            {
                this.DisplayPage();
                _navigator.AskUserKeyDown();
                RefreshSelection(SelectedFunctionId);
            }
        }
        public void AddFooterMessage<T>(IEnumerable<T> objects) where T : class
        {
            _footer += ObjToString.Convert(objects);
        }
        public void AddFooterMessage(DataTable dt)
        {
            string stringDataTable;

            stringDataTable = ObjToString.Convert(dt);
            AddFooterMessage(stringDataTable);
        }
        public void AddFooterMessage(string footerMessage)
        {
            _footer += string.IsNullOrEmpty(footerMessage)
                ? string.Empty
                : $"{footerMessage}\n";
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
                throw new FormatException("Impossible de convertir la valeur en entier");
            }
        }
        public string AskUserValue(string messageToDisplay)
        {
            string output;

            output = MyConsole.AskTypeLine(messageToDisplay);

            return output;
        }
        private void RefreshSelection(int selLine)
        {
            _body = string.Empty;
            SelectedFunctionId = selLine;
            int lineId = 0;

            foreach (string funcTitle in _commandList.Keys)
            {
                _body += $"{GetTitleWithStyle(lineId, funcTitle, IsSelectedId(lineId))} \n";

                if (IsSelectedId(lineId))
                {
                    _loadedDelegate = _commandList.Values.ElementAt(lineId);
                }

                lineId++;
            }
        }
        public void InvokeAction()
        {
            _loadedDelegate?.Invoke();
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
    }
}