using VsConsole.Menu;

namespace VsConsole
{
    internal class ExampleMenu : AMenu
    {
        public ExampleMenu() : base("ExampleMenu")
        {
            //SetupCommands();
        }
        protected  override void SetupCommands()
        {
            this.AddCommand("Hello World Function", DisplayFooterHelloWorld);
        }

        private void DisplayFooterHelloWorld()
        {
            this.AddFooterMessage("Hello World !");
        }
    }
}
