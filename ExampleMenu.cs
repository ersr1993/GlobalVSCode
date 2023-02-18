using VsConsole;

namespace VsConsole
{
    internal class ExampleMenu : AMenu
    {
        public ExampleMenu() : base("ExampleMenu")
        {
            SetupCommands();
        }
        protected  void SetupCommands()
        {
            this.AddCommand("Hello World Function", DisplayFooterHelloWorld);

        }

        private void DisplayFooterHelloWorld()
        {
            this.AddFooterMessage("Hello World !");
        }
    }
}
