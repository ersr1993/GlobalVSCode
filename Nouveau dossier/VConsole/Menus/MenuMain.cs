using VsConsole;
using ConsViSa.Menus.SubMenus;
using System.Collections.Generic;

namespace EpuratedConsole.Console;
public class MenuMain : AMenuMain
{
    public MenuMain(
            MenuMetronom menuMetronome,
            MenuCombo menuCombo,
            MenuCompose menuCompose,
            MenuLesson menuLesson,
            MenuBrouillon menuBrouillon,
            MenuBrouillon2 menuBrouillon2,
            MenuBrouillon3 menuBrouillon3
        ) : base("Menu Principal")
    {
        _subMenus.AddRange
            (new List<IMenu>()
            {
                menuMetronome,
                menuCombo,
                //menuBrouillon3,
                //menuCompose,
                //menuLesson,
                //menuBrouillon,
                //menuBrouillon2,
            }
            );
    }
}
