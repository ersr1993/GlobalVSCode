# GlobalVSCode

This project a toolkit for Console prompt enthousiast.
It allows access to a variety of methods and classes in order to give a quick, simple and epurated console application in .net6.

interface IMenu is used to call every menu;
AMenu is an abstract class for inheriting from menu like behaviour.

Try it out with following syntax :

using VsConsole;
namespace SimpleConsole.Menus;
public class MenuBrouillon : AMenu, IMenu
{
    public MenuBrouillon() : base("Menu Brouillon")
    {}
    protected override void SetupCommands()
    {
        this.AddCommand(SomeFunction_YouCanCall);
        this.AddHelloWorld();
    }
    private void SomeFunction_YouCanCall()
    {
        this.AddFooterMessage("You nailed it !");
    }
    private void OpenOtherMenu()
    {
        IMenu nextMenu;
        nextMenu = new MenuBrouillon();
        nextMenu.Open();
        this.AddFooterMessage("Welcome back to original Menu");
    }
}
