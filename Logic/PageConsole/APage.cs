#nullable disable
namespace VsConsole.Logic.PageConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using VsConsole.Data;
    public abstract class APage : IPage
    {
        public virtual string _title { get; protected set; }
        public virtual string _body { get; protected set; }
        public List<(string, ConsoleColor?)> _footerItems { get; protected set; }
        public virtual void DisplayPage()
        {

            Console.Title = _title;
            try
            {
                Console.Clear();
            }
            catch (IOException ex)
            {
                string msg = "Erreur causée par la position de la fenêtre console, allongée le long d'un côté";
                throw new Exception(msg,innerException:ex);
            }

            MyConsole.MyWriteLine(_title, ConsoleColor.Yellow);

            MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);
            Console.WriteLine(_body);
            MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);

            if (_footerItems != null)
                MyConsole.WriteLines(_footerItems);
            MyConsole.MyWriteLine($"{MsgOut.GetMenuActionsInstruction()} ", ConsoleColor.DarkMagenta); // Je l'aime Bien
        }
        public virtual void ClearFooter()
        {
            this._footerItems = new List<(string, ConsoleColor?)>();
            //this._footer = string.Empty;
        }

    }
}
