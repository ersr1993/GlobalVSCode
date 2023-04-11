using System.Collections.Generic;

namespace VsConsole
{
    public abstract class AMenuMain : AMenu
    {
        protected List<IMenu> _subMenus;
        public AMenuMain() : base("Menu principal")
        {
            _subMenus = new List<IMenu>();
        }
        protected void CommitSubMenus()
        {
            foreach (IMenu menu in _subMenus)
            {
                string title;
                title = menu.GetType().Name;
                AddCommand(title, menu.Open);
            }
        }
    }
}
