using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsConsole;

namespace DevBackImprOoViSa
{
    internal class MenuMain : AMenu
    {
        public MenuMain() : base("Menu Principal")
        {
            this.AddCommand("HelloWorld", SayHello);
            this.DisplayLoop();
        }

        private void SayHello()
        {
            this.AddFooterMessage("HelloWorld");
        }
    }
}
