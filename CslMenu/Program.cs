using System;
using System.Collections;

namespace CslMenu{
   
    using _cons = myConsole;
    using _menu = myMenu;
    class Program
    {

        static void Main(string[] args)
        {
            _cons.Menu menu0;
            //-
            _cons.MyConsole.Clear();
            menu0 = new _menu.Main(" Menu SMART ");

            menu0.DisplayMenu();
            //-
            // GoodByeText();
        }	
    }
    
}
