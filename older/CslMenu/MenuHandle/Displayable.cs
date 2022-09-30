namespace myConsole{

	using System;
    public static class MyConsole{
		public static void Clear()
		{
			Console.Clear();
		}
		public static ConsoleKey DisplayPage(IPage somePage){
			Clear();
			Console.WriteLine(somePage.title);
			AddLines();
			Console.WriteLine(somePage.content);
			AddLines();
			Console.WriteLine(somePage.footer);
			//-
			ConsoleKeyInfo myChar;
			myChar = Console.ReadKey();
			return myChar.Key;
		}
		private static void AddLines(){
			Console.WriteLine("--------------\n ");
		}
		public static void SayAndWait(string message){ 
			Console.WriteLine(message);
			Console.ReadKey();
		}
		public static string AskTypeLine(string question){
			string typedKey;
			//-
			Console.WriteLine(question);
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

