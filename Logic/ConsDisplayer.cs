namespace MyConsMenu
{

    using System;
    public static class MyConsole
    {
        public static ConsoleKey DisplayPage(IPage somePage)
        {

            Console.Clear();
            Console.Title = somePage.title;
            Console.WriteLine(somePage.title);

            AddDottedLines();
            Console.WriteLine(somePage.content);
            AddDottedLines();
            
            //MyWriteLine( $"{somePage.footer} \n \n \n  ",ConsoleColor.Blue );
            MyWriteLine( $"{MsgOut.FooterMsg()} ",ConsoleColor.DarkMagenta); // Je l'aime Bien
            MyWriteLine( $"{MsgOut.FooterMsg()} ",ConsoleColor.DarkCyan );
            //-
            ConsoleKeyInfo myChar;
            myChar = Console.ReadKey();
            return myChar.Key;
        }
        private static void MyWriteLine(string someStr, ConsoleColor myColor=ConsoleColor.White)
        {
            Console.ForegroundColor = myColor;

            Console.WriteLine(someStr,myColor);

            Console.ResetColor();
        }
        private static void AddDottedLines()
        {
            MyWriteLine("--------------\n ", ConsoleColor.DarkYellow);
        }
        public static void Say(string message)
        {
            MyWriteLine(message);
        }
        public static void SayAndWait(string message)
        {
            Console.WriteLine("\n Output : \n" + message);
            Console.ReadKey();
        }
        public static string AskTypeLine(string question)
        {
            string typedKey;
            //-
            MyWriteLine(question,ConsoleColor.DarkYellow);
            typedKey = Console.ReadLine();
            //-
            return typedKey;
        }
        public static string OtherString()
        {
            return "otherString";
        }
    }
}

