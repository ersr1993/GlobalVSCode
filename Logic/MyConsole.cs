namespace VsConsole.Logic
{

    using System;
    using System.Runtime.CompilerServices;
    using VsConsole.Data;
    using VsConsole.Logic.PageConsole;

    internal  static class MyConsole
    {
        private const ConsoleColor colorDefault = ConsoleColor.White;
        internal static ConsoleKey AskKey()
        {
            ConsoleKeyInfo myChar;
            myChar = Console.ReadKey();
            return myChar.Key;
        }
        internal static void MyWriteLine(string someStr, ConsoleColor myColor = colorDefault)
        {
            Console.ForegroundColor = myColor;

            Console.WriteLine(someStr, myColor);

            Console.ResetColor();
        }
        internal static void Say(string message,ConsoleColor color = colorDefault)
        {
            MyWriteLine(message,color);
        }
        internal static void SayAndWait(string message,ConsoleColor color = colorDefault) 
        {
            MyWriteLine($"\n Output : \n {message}",color);
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

