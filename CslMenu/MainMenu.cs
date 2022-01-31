
namespace myMenu{
using System;
using _cons=myConsole;

    class Main: _cons.Menu{

        public Main(string title): base(title) {
            this.commands.Clear();
            this.commands.Add("one",First);
            this.commands.Add("two",Second);
            this.commands.Add("three",First);
            FillContent();           
        }
        private void First(){
            myMenu.Main nextMenu;
            //-
            nextMenu = new Main("second");
            //-
            nextMenu.DisplayMenu();
        }
        private void Second(){

        }
    }
}