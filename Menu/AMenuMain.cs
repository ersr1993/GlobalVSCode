using System;
using System.Collections.Generic;
using System.Linq;

namespace VsConsole
{
    public abstract class AMenuMain : AMenu, IMenu
    {
        protected List<IMenu> _subMenus { get; set; }
        public AMenuMain(string title = "Menu principal") : base(title)
        {
            _subMenus = new List<IMenu>();
        }
        protected override void SetupCommands()
        {
            CheckSomeMenusExist();
            foreach (IMenu menu in _subMenus)
            {
                string title;
                title = menu.GetType().Name;
                AddCommand(title, menu.Open);
            }
        }
        private void CheckSomeMenusExist()
        {
            if (_subMenus == null || !_subMenus.Any())
            {
                DisplayNoMenuMessages();
            }
        }
        private void DisplayNoMenuMessages()
        {
            string msg;
            char chevronOpen = '[';
            char chevronClose = ']';
            msg = $"Afin d'utiliser le menu principal, pensez à implémenter les menus " +
                $"de la liste _subMenus dans le constructeur par exemple. \n" +
                "ex : this._subMenus = new List<IMenu>()\n" +
                $"{chevronOpen} \n" +
                "   monMenuA,\n" +
                "   monMenuB\n"
                + $"{chevronClose}";
            this.AddFooterMessage(msg, ConsoleColor.Red);
        }
    }
}
