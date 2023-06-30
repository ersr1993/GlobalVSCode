using System;
using System.Collections.Generic;
using System.Linq;

namespace VsConsole.Logic;
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
    internal static NavigationChoice _userKeyDown { get; set; } // static allows cascadate quit

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
                { ConsoleKey.NumPad0, ()=>Nav(ConsoleKey.NumPad0) },
                { ConsoleKey.NumPad1, ()=>Nav(ConsoleKey.NumPad1) },
                { ConsoleKey.NumPad2, ()=>Nav(ConsoleKey.NumPad2) },
                { ConsoleKey.NumPad3, ()=>Nav(ConsoleKey.NumPad3) },
                { ConsoleKey.NumPad4, ()=>Nav(ConsoleKey.NumPad4) },
                { ConsoleKey.NumPad5, ()=>Nav(ConsoleKey.NumPad5) },
                { ConsoleKey.NumPad6, ()=>Nav(ConsoleKey.NumPad6) },
                { ConsoleKey.NumPad7, ()=>Nav(ConsoleKey.NumPad7) },
                { ConsoleKey.NumPad8, ()=>Nav(ConsoleKey.NumPad8) },
                { ConsoleKey.NumPad9, ()=>Nav(ConsoleKey.NumPad9) },
            };
    }

    private NavigationChoice Nav(ConsoleKey x)
    {
        try
        {
            int numP;
            string numpadString;
            int total;
            bool isTooBigInt;
            total = _menu.CountCommands();
            numpadString = x.ToString().Last().ToString();
            numP = int.Parse(numpadString);
            isTooBigInt = numP > total - 1;
            if (!isTooBigInt)
            {
                _menu.RefreshSelection(numP);
                //InvokeFunc();
            }
            else
            {
                _menu.SelectedFunctionId = total - 1;
            }
        }
        catch (Exception e)
        {
            _menu.SelectedFunctionId = 0;
        }
        return NavigationChoice.stay;
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
        NavigationChoice onKeyDown;
        onKeyDown= NavigationChoice.stay;
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
            //throw;
        }

        return onKeyDown;
    }
    private NavigationChoice InokeNavigation(ConsoleKey usrInput)
    {
        Func<NavigationChoice> selectedFunction;
        NavigationChoice choice;
        Func<NavigationChoice>[] temp;

        temp = (from kpv in _menuButtonsActions
                where kpv.Key == usrInput
                select kpv.Value).ToArray();
        try
        {
            selectedFunction = temp[0];
            choice = selectedFunction.Invoke();
        }
        catch (IndexOutOfRangeException)
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
        if (!IsSelected_AtBottom(_menu.SelectedFunctionId))
        {
            _menu.SelectedFunctionId++;
        }

        return NavigationChoice.stay;
    }
    private bool IsSelected_AtBottom(int selectedId)
    {
        bool isBottom;
        isBottom = _menu.SelectedFunctionId + 2 > _menu.CountCommands();
        return isBottom;
    }
    private NavigationChoice NavUp()
    {
        //bool isTop;
        //isTop = _menu.SelectedFunctionId > 0;
        if (IsTop(_menu.SelectedFunctionId))
        {
            _menu.SelectedFunctionId--;
        }
        return NavigationChoice.stay;
    }
    private bool IsTop(int selectedId)
    {
        bool isTop;
        isTop = selectedId > 0;
        return isTop;
    }

}
