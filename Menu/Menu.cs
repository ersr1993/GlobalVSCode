namespace VsConsole
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Data;
    using VsConsole.Logic.PageConsole;
    using VsConsole.Logic;

    public abstract class AMenu : APage, IMenu
    {
        public int SelectedFunctionId { get; set; }

        protected Dictionary<string, Action> _commandList;
        protected NavigationLogic _navigator;
        protected Action? _loadedDelegate;
        public AMenu()
        {
            _commandList = new Dictionary<string, Action>();
            _navigator = new NavigationLogic(this);
            _title = GetType().ToString();
        }
        public AMenu(string someTitle) : this()
        {
            _title = someTitle;
        }

        public void AddCommand(string name, Action myDelegateFunction)
        {
            _commandList.Add(name, myDelegateFunction);
            RefreshSelection(SelectedFunctionId);
        }
        public void DisplayLoop()
        {
            while (_navigator.StayInLoop())
            {
                this.DisplayPage();
                _navigator.AskUserKeyDown();
                RefreshSelection(SelectedFunctionId);
            }
        }
        public void AddFooterMessage(string footerMessage)
        {
            _footer += string.IsNullOrEmpty(footerMessage)
                ? string.Empty
                : $"{footerMessage}\n";
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
            _loadedDelegate.Invoke();
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
                outputString = $"{id} *  {lineOfText.ToUpper()}  ***";
            }
            else
            {
                outputString = $"{id} -  {lineOfText.ToLower()}";
            }

            return outputString;
        }
        public int CountCommands()
        {
            return this._commandList.Count;
        }
    }
}