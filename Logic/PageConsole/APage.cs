#nullable disable
namespace VsConsole.Logic.PageConsole
{
    using System;
    using System.Collections.Generic;
    using VsConsole.Data;
    public abstract class APage : IPage
    {
        public virtual string _title { get; protected set; }
        public virtual string _body { get; protected set; }
        public List<(string, ConsoleColor?)> _footerItems { get; protected set; }
        public virtual void DisplayPage()
        {

            Console.Title = _title;
            Console.Clear();

            MyConsole.MyWriteLine(_title, ConsoleColor.Yellow);

            MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);
            Console.WriteLine(_body);
            MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);

            if(_footerItems!=null)MyConsole.WriteLines(_footerItems);
            MyConsole.MyWriteLine($"{MsgOut.GetMenuActionsInstruction()} ", ConsoleColor.DarkMagenta); // Je l'aime Bien
        }
        public virtual void ClearFooter()
        {
            this._footerItems = new List<(string, ConsoleColor?)>();
            //this._footer = string.Empty;
        }

    }
}
