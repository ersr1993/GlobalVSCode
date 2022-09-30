namespace MyConsMenu.Examples
{
	using System;

	public class ExampleMain: Menu{

		public ExampleMain(string title): base(title) {
			this.commandList.Clear();
			this.commandList.Add("one",First);
			this.commandList.Add("two",Second);
			this.commandList.Add("three",First);
			//this.FillContent();
			Display();
		}
		private void First(){
			ExampleMain nextMenu;
			//-
			nextMenu = new ExampleMain("second");
			//-
			//nextMenu.DisplayMenu();
		}
		private void Second(){
			ExampleMain nextMenu;
			//-
			nextMenu = new ExampleMain("third");
			//-
			//nextMenu.DisplayMenu();
		}
	}
}