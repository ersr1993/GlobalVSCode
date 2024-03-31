using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsConsole;

namespace ConsViSa.Menus.SubMenus;
public class MenuBrouillon3 : AMenu
{
    public MenuBrouillon3() { }
    protected override void SetupCommands()
    {
        this.AddHelloWorld();
    }
}
