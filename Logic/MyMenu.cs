namespace MyConsMenu
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using _cons = MyConsMenu;
    using System.Data;
    using System.Xml.Schema;

    enum select
    {
        nextMenu,
        backOne,
        stay,
        quit
    }
    public abstract class Menu : _cons.IPage, IMenu
    {
        public delegate void DelFunction();
        public string funcName { get; private set; }
        public string content { get; private set; }
        public string title { get; private set; } = "";
        public string footer { get; set; }
        private int currentSelected = 0;
        private static select userSelection;
        DelFunction commandLoaded;
        protected Dictionary<string, DelFunction> commandList;
        //---------
        public Menu()
        {
            //this.footer += MsgOut.FooterMsg();
            commandList = new Dictionary<string, DelFunction>();
            currentSelected = 0;

        }
        public Menu(string someTitle) : this()
        {
            this.title = someTitle;
        }

        public void AddCommand(string name, DelFunction myDelegateFunction)
        {
            this.commandList.Add(name, myDelegateFunction);
            this.UpdateCmdDisplay();
        }

        public void AddFooterMessage(string footerMessage)
        {
            this.footer += footerMessage + "\n";
        }
        public void AddFooterMessage(DataTable dt)
        {
            string stringRow;

            stringRow = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                stringRow += $"{dt.Columns[i].ColumnName.ToUpper(),15}";
            };
            stringRow += "\n";
            foreach (DataRow r in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    stringRow += $"{r[i].ToString(),15}";
                    stringRow += " ;  ";
                }
                stringRow += "\n";
            }
            this.AddFooterMessage(stringRow);
        }
        public float AskUserFloat(string messageToDisplay)
        {
            string outputAsString;

            outputAsString = this.AskUserValue(messageToDisplay);
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

        // ---------
        private void UpdateCmdDisplay()
        {
            int idFunc;
            string labelFunc;

            idFunc = 0;
            this.content = string.Empty;

            foreach (KeyValuePair<string, DelFunction> menuItem in this.commandList)
            {

                labelFunc = menuItem.Key;
                if (idFunc == this.currentSelected)
                {
                    PimpSelectedCommand(ref labelFunc);
                    this.commandLoaded = this.commandList.Values.ElementAt(idFunc);
                }
                else
                {
                    labelFunc = labelFunc.ToLower();
                }

                this.content += idFunc + " : " + labelFunc + "\n";
                idFunc++;
            }
        }
        private void PimpSelectedCommand(ref string myString)
        {
            myString = "* " + myString.ToUpper() + " ***";
        }
        public void Display(string message = null)
        {
            select userAction;
            //-
            while (userSelection != select.quit &&
                  userSelection != select.backOne)
            {
                ConsoleKey userChoice;
                bool prevChoiceWastoQuit;
                //-
                //this.footer = message + "\n\n" + this.footer;
                this.footer = (message == null || message == string.Empty) ? $"{this.footer}" : $"\n\n {message}";
                //this.footer = this.footer + message;
                userChoice = _cons.MyConsole.DisplayPage(this);
                //this.footer = String.Empty;
                prevChoiceWastoQuit = userSelection == select.quit;
                if (!prevChoiceWastoQuit)
                {
                    try
                    {
                        userAction = Execute(userChoice);
                    }
                    catch (Exception e)
                    {
                        _cons.MyConsole.SayAndWait("\n \n  *** *** *** ERROR : *** *** ***" +
                            " \n - Message : " + e.Message + "\n" +
                            " \n - StackTrace : \n " + e.StackTrace);
                        userAction = select.stay;
                    }

                    bool userWantsToQUIT;

                    userWantsToQUIT = (userAction == select.quit);

                    if (!userWantsToQUIT)
                    {
                        userSelection = userAction;
                    }
                    else
                    {
                        userSelection = select.quit;
                    }
                }
            }
        }
        private select Execute(ConsoleKey usrInput)
        {
            select userAct;
            userAct = select.quit;

            switch (usrInput)
            {
                case (System.ConsoleKey.C):
                    this.footer = string.Empty;
                    userAct = select.stay;
                    break;
                case (System.ConsoleKey.DownArrow):
                case (System.ConsoleKey.J):
                    bool isNotLastFunc;

                    isNotLastFunc = (this.commandList.Count - 1 > this.currentSelected);

                    if (isNotLastFunc)
                    {
                        this.currentSelected++;
                        UpdateCmdDisplay();
                    }

                    userAct = select.stay;
                    break;
                case (System.ConsoleKey.UpArrow):
                case (System.ConsoleKey.K):

                    if (currentSelected > 0)
                    {
                        this.currentSelected--;
                        UpdateCmdDisplay();
                    }

                    userAct = select.stay;
                    break;
                case System.ConsoleKey.Enter:
                case System.ConsoleKey.Spacebar:

                    this.commandLoaded.Invoke();
                    if (userSelection != select.quit)
                    {
                        userAct = select.nextMenu;
                    }

                    break;
                case System.ConsoleKey.B:
                    userAct = select.backOne;
                    break;
                case ConsoleKey.Q:
                    currentSelected = -1;
                    userAct = select.quit;
                    userSelection = select.quit;
                    break;
                default:
                    userAct = select.stay;
                    break;
            }
            return userAct;
        }
    }
}