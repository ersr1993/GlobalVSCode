namespace myMenu{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using _cons = myConsole;
    enum select{
        nextMenu,
        backOne,
        stay,
        quit
    }
    public abstract class Menu : _cons.IPage {
        protected delegate void funcDel();
        public string funcName {get ; private set;}
        public string content {get; private set;}
        public string title {get; private set;}
        public string footer{get; private set;}
        private int currentSelected =0;
        private static select userSelection;
        funcDel callMe;
        protected Dictionary<string,funcDel> commands;
        public Menu(string someTitle)
        {
            this.title = someTitle;
            this.footer = "Enter  ...\n b : Menu Back";
            commands = new Dictionary<string, funcDel>();
            currentSelected = 0;
        }
        protected virtual void FillContent()
        {
            Dictionary<string,funcDel>.Enumerator allCmds;
            //-
            this.content = string.Empty;
            allCmds = commands.GetEnumerator();
            int i =0; 
            foreach( KeyValuePair<string,funcDel> menuItem in commands)
            {
                string asDisplayedFunc;
                //-
                asDisplayedFunc = menuItem.Key;
                if(i == currentSelected)
                {
                    asDisplayedFunc = "* "+asDisplayedFunc.ToUpper() + " ***";
                    callMe = commands.Values.ElementAt(i);
                }
                else{
                    asDisplayedFunc = asDisplayedFunc.ToLower();
                }
                //-
                this.content += i+ " : " + asDisplayedFunc +"\n"; 
                i++;
            }
            
        }
        private void FirstFunc()
        {
            _cons.MyConsole.SayAndWait("Here is some real nice function");
        }
        private void OtherFunc(){
            _cons.MyConsole.SayAndWait("but you could have chosen nb 1");
        }
        public void DisplayMenu()
        {
            ConsoleKey choice;
            select  userAction;
            while(userSelection != select.quit && userSelection != select.backOne)
            { 
                choice = _cons.MyConsole.DisplayPage(this);
                if(userSelection != select.quit ){
                // if(choice != ConsoleKey.Q ){
                userAction = Execute(choice);
                if(userSelection != select.quit) userSelection = userAction;}
                else userSelection = select.quit;
            }
        }
        
        private select Execute(ConsoleKey myAction)
        {
            select userAct = select.quit;
            //-
            switch (myAction)
            {
                case  (System.ConsoleKey.DownArrow) :
                    if(commands.Count-1>currentSelected)
                    { 
                        currentSelected ++;
                        FillContent();
                    }
                        userAct = select.stay;
                break;
                case (System.ConsoleKey.UpArrow) :
                    if( currentSelected>0) {
                        currentSelected =(currentSelected-1);
                        FillContent();
                        }
                        userAct = select.stay;
                    break;
                case System.ConsoleKey.Enter:
                    callMe.Invoke();
                    userAct = select.nextMenu;
                    break;
                case System.ConsoleKey.B:
                    userAct = select.backOne;
                break;
                case ConsoleKey.Q:
                    currentSelected = -1;
                    userAct = select.quit;
                    break;
                default :
                    userAct = select.stay;
                break;
            }
            return userAct;
            // choice != System.ConsoleKey.Q
        }
    }
}