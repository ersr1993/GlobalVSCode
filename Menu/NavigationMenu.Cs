using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsConsole.Logic.PageConsole;

namespace VsConsole.Logic
{
    internal enum NavigationChoice
    {
        nextMenu,
        backOne,
        stay,
        quit
    }

    public class NavigationLogic
    {
        private IMenu _menu;
        private Dictionary<ConsoleKey, Func<NavigationChoice>> _menuButtonsActions;
        private static NavigationChoice _userKeyDown; // static allows cascadate quit

        internal NavigationLogic(IMenu menu)
        {
            _menu = menu;
            _menu.SelectedFunctionId = 0;
            _menuButtonsActions = GetMenuButtonsActions();
        }
        private Dictionary<ConsoleKey, Func<NavigationChoice>> GetMenuButtonsActions()
        {
            return new Dictionary<ConsoleKey, Func<NavigationChoice>>()
            {
                { ConsoleKey.C,ClearFooter},
                { ConsoleKey.K, NavUp},
                { ConsoleKey.UpArrow, NavUp },
                { ConsoleKey.J, NavDown },
                { ConsoleKey.DownArrow, NavDown },
                { ConsoleKey.Spacebar, InvokeFunc },
                { ConsoleKey.Enter, InvokeFunc },
                { ConsoleKey.B, BackMenu },
                { ConsoleKey.Q, QuitProgram},
                { ConsoleKey.Escape, QuitProgram },
            };
        }
        public bool StayInLoop()
        {
            bool stayInLoop;

            stayInLoop = _userKeyDown != NavigationChoice.quit &&
                         _userKeyDown != NavigationChoice.backOne;

            return stayInLoop;
        }

        internal void AskUserKeyDown()
        {
            bool userQuits;
            ConsoleKey keyDown;

            keyDown = MyConsole.AskKey();
            userQuits = _userKeyDown == NavigationChoice.quit;
            if (userQuits)
            {
                _userKeyDown =  NavigationChoice.quit;
            }
            else
            {
                _userKeyDown = SafeNavigate(keyDown);
            }
        }
        private NavigationChoice SafeNavigate(ConsoleKey keyDown)
        {
            NavigationChoice onKeyDown = NavigationChoice.stay;
            try
            {
                onKeyDown = InokeNavigation(keyDown);
            }
            catch (Exception e)
            {
                MyConsole.Say($"\n \n  *** *** *** ERROR : *** *** *** \n " +
                    $"-  {e.Message} + \n\n ", ConsoleColor.Red);
                MyConsole.SayAndWait($" \n - StackTrace : \n  {e.StackTrace}", ConsoleColor.DarkMagenta);
            }

            return onKeyDown;
        }
        private NavigationChoice InokeNavigation(ConsoleKey usrInput)
        {
            Func<NavigationChoice> selectedFunction;

            var temp = (from kpv in _menuButtonsActions
                        where kpv.Key == usrInput
                        select kpv.Value).ToArray();
            try
            {
                selectedFunction = temp[0];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new InvalidOperationException($"Touche '{usrInput}' non reconnue");
            }

            return selectedFunction.Invoke();
        }
        private NavigationChoice QuitProgram()
        {
            return NavigationChoice.quit;
        }
        private NavigationChoice BackMenu()
        {
            return NavigationChoice.backOne;
        }
        private NavigationChoice InvokeFunc()
        {
            NavigationChoice userAct = NavigationChoice.quit;
            //_loadedDelegate.Invoke();
            _menu.InvokeAction();
            if (_userKeyDown != NavigationChoice.quit)
            {
                userAct = NavigationChoice.nextMenu;
            }
            return userAct;
        }
        private NavigationChoice ClearFooter()
        {
            _menu.ClearFooter();
            return NavigationChoice.stay;
        }
        private NavigationChoice NavDown()
        {
            bool isBottom;

            isBottom = _menu.SelectedFunctionId + 1 >= _menu.CountCommands();
            if (!isBottom)
            {
                _menu.SelectedFunctionId++;
            }

            return NavigationChoice.stay;
        }
        private NavigationChoice NavUp()
        {
            bool isTop;
            isTop = _menu.SelectedFunctionId > 0;
            if (isTop)
            {
                _menu.SelectedFunctionId--;
            }

            return NavigationChoice.stay;
        }

    }
}
