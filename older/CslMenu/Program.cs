using System;
using System.Collections;

namespace CslMenu{
    
    using _menu = myMenu;
    class Program
    {
        static void Main(string[] args)
        {
            _menu.Menu menu0;
            //-
            menu0 = new _menu.Main(" Menu SsMART ");

            menu0.DisplayMenu();
            //-
            // GoodByeText();
        }	
    }
}
