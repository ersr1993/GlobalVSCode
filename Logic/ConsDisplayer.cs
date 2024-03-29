//namespace VsConsole
//{

//    using System;
//    using VsConsole.Logic.PageConsole;
//    using VsConsole.Data;
//    using System.Collections.Generic;

//    public static class MyConsole
//    {
//        public static ConsoleKey DisplayPage(IPage somePage)
//        {

//            Console.Clear();
//            Console.Title = somePage._title;
//            MyWriteLine(somePage._title);

//            AddDottedLines();
//            MyWriteLine(somePage._body);
//            AddDottedLines();

//            //MyWriteLine( $"{somePage._footer} \n \n \n  ",ConsoleColor.DarkMagenta); // Je l'aime Bien

//            WriteLines(somePage._footerItems);
//            //MyWriteLine( $"{somePage._footer} \n \n \n  ",ConsoleColor.DarkMagenta); // Je l'aime Bien
//            MyWriteLine( $"{MsgOut.GetMenuActionsInstruction()} ",ConsoleColor.DarkCyan );
//            //-
//            ConsoleKeyInfo myChar;
//            myChar = Console.ReadKey();
//            return myChar.Key;
//        }
//        private static void WriteLines(List<(string msg,ConsoleColor? color)> lines)
//        {
//            foreach (var line in lines)
//            {
//                Console.WriteLine(line.Item1,line.Item2);
//            }
//        }
//        public static void MyWriteLine(string someStr, ConsoleColor myColor=ConsoleColor.White)
//        {
//            Console.ForegroundColor = myColor;

//            Console.WriteLine(someStr,myColor);

//            Console.ResetColor();
//        }
//        private static void AddDottedLines()
//        {
//            MyWriteLine("--------------\n ", ConsoleColor.DarkYellow);
//        }
//        public static void Say(string message)
//        {
//            MyWriteLine(message);
//        }
//        public static void SayAndWait(string message)
//        {
//            Console.WriteLine("\n Output : \n" + message);
//            Console.ReadKey();
//        }
//        public static string AskTypeLine(string question)
//        {
//            string typedKey;
//            //-
//            MyWriteLine(question,ConsoleColor.DarkYellow);
//            typedKey = Console.ReadLine();
//            //-
//            return typedKey;
//        }
//        public static string OtherString()
//        {
//            return "otherString";
//        }
//    }
//}

