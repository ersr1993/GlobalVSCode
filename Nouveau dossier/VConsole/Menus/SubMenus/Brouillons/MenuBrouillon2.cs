using StandardTools.Analysis;
using VsConsole;

namespace ConsViSa.Menus.SubMenus;

public class MenuBrouillon2 : AMenu
{
    private GameScene Scene { get; set; }
    private GameEngine _engine { get; set; }
    public MenuBrouillon2(
            GameScene scene,
            GameEngine engine
        )
    {
        Scene = scene;
        _engine = engine;
    }
    protected override void SetupCommands()
    {
        this.AddCommand(_engine.StartGame);
        this.AddCommand(Scene.Play);
    }
}
