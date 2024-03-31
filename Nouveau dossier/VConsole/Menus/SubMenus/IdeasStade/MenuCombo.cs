using VsConsole;

namespace ConsViSa.Menus.SubMenus;

public class MenuCombo : AMenu
{
    public MenuCombo() : base("MenuCombo") { }

    protected override void SetupCommands()
    {
        //AddChoi
        AddCommand(() => throw new System.Exception());
        //this.AddChoice
        AddHelloWorld();
        AddHelloWorld();
    }

    private string[] ChoiceList = new string[]
    {
        "a",
        "c",
        "b",
    };
}
