namespace VsConsole.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using VsConsole.Data;
    using VsConsole.Logic.PageConsole;

    internal static class MyConsole
    {
        private const ConsoleColor _CONSOLE_COLOR = ConsoleColor.Green;
        internal static ConsoleKey AskKey()
        {
            ConsoleKeyInfo myChar;
            myChar = Console.ReadKey();
            return myChar.Key;
        }
        internal static void MyWriteLine(string someStr, ConsoleColor myColor = _CONSOLE_COLOR)
        {
            Console.ForegroundColor = myColor;
            Console.WriteLine(someStr, myColor);
            Console.ResetColor();
        }
        internal static void WriteLines(List<(string msg, ConsoleColor? color)> lines)
        {
            foreach ((string msg, ConsoleColor? color) line in lines)
            {
                if (line.color == null)
                {
                    MyWriteLine(line.msg);
                }
                else
                {
                    ConsoleColor color;
                    color = (ConsoleColor)line.color;
                    MyWriteLine(line.msg, color);
                }
            }
        }
        internal static void Say(string message, ConsoleColor color = _CONSOLE_COLOR)
        {
            MyWriteLine(message, color);
        }
        internal static void SayAndWait(string message, ConsoleColor color = _CONSOLE_COLOR)
        {
            MyWriteLine($"\n Output : \n {message}", color);
            Console.ReadKey();
        }
        internal static string AskTypeLine(string question)
        {
            string typedKey;
            //-
            MyWriteLine(question, ConsoleColor.DarkYellow);
            typedKey = Console.ReadLine();
            //-
            return typedKey;
        }
    }

}
