#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VsConsole.Data;

namespace VsConsole.Logic.PageConsole;
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
            string msg;
            msg = "Erreur causée par la position de la fenêtre console.";
            throw new Exception(msg, innerException: ex);
        }

        List<(string, ConsoleColor?)> allLines;
        allLines = new List<(string, ConsoleColor?)>
                   {
                        (_title, ConsoleColor.Yellow),
                        (MsgOut.DottedLines(), ConsoleColor.Yellow),
                        (_body, ConsoleColor.White),
                        (MsgOut.DottedLines(), ConsoleColor.Yellow),
                   };

        //MyConsole.MyWriteLine(_title, ConsoleColor.Yellow);
        //MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);
        //MyConsole.MyWriteLine(_body, ConsoleColor.White);
        //MyConsole.MyWriteLine(MsgOut.DottedLines(), ConsoleColor.Yellow);

        if (_footerItems != null)
        {
            allLines.AddRange(_footerItems);
        }
        //MyConsole.MyWriteLine($"{MsgOut.GetMenuActionsInstruction()} ", ConsoleColor.DarkMagenta); // Je l'aime Bien
        allLines.Add(($"{MsgOut.GetMenuActionsInstruction()} ", ConsoleColor.DarkMagenta));
        MyConsole.WriteLines(allLines);
        
    }
    public virtual void ClearFooter()
    {
        this._footerItems.Clear();
    }

}
