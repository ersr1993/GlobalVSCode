using VsConsole;

namespace VsConsole
{
    internal class ExampleMenu : AMenu
    {
        public ExampleMenu() : base("ExampleMenu")
        {
            this.AddCommand("Hello World Function", DisplayFooterHelloWorld);
            this.DisplayLoop();
        }

        private void DisplayFooterHelloWorld()
        {
            this.AddFooterMessage("Hello World !");
        }
    }
}
