using System;

namespace someProject
{
    using _menu = myMenu;
    class Program{
            static void Main(string[] args)
        {
            _menu.Menu menu0;
            //-
            menu0 = new _menu.Main(" here we go  ");
            //-
            menu0.DisplayMenu();
            // GoodByeText();
        }	
}
}
