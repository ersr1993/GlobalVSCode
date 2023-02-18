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
        private AMenu _menu;
        private Dictionary<ConsoleKey, Func<NavigationChoice>> _menuButtonsActions;
        private static NavigationChoice _userKeyDown; // static allows cascadate quit

        internal NavigationLogic(AMenu menu)
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
                _userKeyDown = NavigationChoice.quit;
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
                string errorMessage;
                errorMessage = $"\n \n  *** *** *** ERROR : *** *** *** \n " +
                               $"-  {e.Message} \n\n ";
                _menu.AddFooterMessage(errorMessage, ConsoleColor.Red);
#if DEBUG
                string fullInfo;
                fullInfo = $" \n - StackTrace : \n  " +
                            $"{e.StackTrace}";
                MyConsole.Say(errorMessage, ConsoleColor.Red);
                MyConsole.SayAndWait(fullInfo, ConsoleColor.DarkYellow);
#endif
            }

            return onKeyDown;
        }
        private NavigationChoice InokeNavigation(ConsoleKey usrInput)
        {
            Func<NavigationChoice> selectedFunction;
            NavigationChoice choice;

            Func<NavigationChoice>[] temp = (from kpv in _menuButtonsActions
                                             where kpv.Key == usrInput
                                             select kpv.Value).ToArray();
            try
            {
                selectedFunction = temp[0];
                choice = selectedFunction.Invoke();
            }
            catch (IndexOutOfRangeException ex)
            {
                string message;
                message = $"Touche '{usrInput}' non reconnue";
                MyConsole.SayAndWait(message, ConsoleColor.DarkYellow);
                choice = NavigationChoice.stay;
            }

            return choice;
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
            NavigationChoice userAct;

            userAct = NavigationChoice.quit;
            _menu.LoadedDelegate?.Invoke();
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
