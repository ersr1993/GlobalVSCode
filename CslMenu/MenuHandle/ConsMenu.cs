namespace myMenu{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using _cons = myConsole;
    public abstract class Menu : _cons.IPage {
        protected delegate void funcDel();
        public string funcName {get ; private set;}
        public string content {get; private set;}
        public string title {get; private set;}
        public string footer{get; private set;}
        private int currentSelected =0;
        funcDel callMe;
        protected Dictionary<string,funcDel> commands;
        public Menu(string someTitle)
        {
            this.title = someTitle;
            this.footer = "Enter  ...\n Press q does not work";
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
            do{
                choice = _cons.MyConsole.DisplayPage(this);
                Execute(choice);}
            while(choice != System.ConsoleKey.Q);
        }

        private void Execute(ConsoleKey myAction)
        {
            switch (myAction)
            {
                case  (System.ConsoleKey.DownArrow) :
                    if(commands.Count-1>currentSelected)
                    { 
                        currentSelected ++;
                        FillContent();
                    }
                break;
                case (System.ConsoleKey.UpArrow) :
                    if( currentSelected>0) {
                        currentSelected =(currentSelected-1);
                        FillContent();
                    }
                    break;
                case System.ConsoleKey.Enter:
                    callMe.Invoke();
                    break;
                case System.ConsoleKey.B:
                break;
                case ConsoleKey.Q:
                    currentSelected = -1;
                    break;
                default :
                break;
            }
        }

    }
}