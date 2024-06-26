﻿
using System;
using System.Collections.Generic;
using System.Linq;
using VsConsole.Logic;

namespace VsConsole.Menu;

public class NavigationLogic
{
    //private AMenu _menu { get; set; }
    //private CommandActions _actions { get { return _menu._actions; } }
    private CommandActions _actions { get; set; }

    private Dictionary<ConsoleKey, Func<NavigationChoice>> _menuButtonsActions { get; set; }
    internal static NavigationChoice _userKeyDown { get; set; } // static allows cascadate quit

    internal NavigationLogic(CommandActions actions)
    {
        _actions = actions;
        _menuButtonsActions = GetMenuButtonsActions();
    }

    private Dictionary<ConsoleKey, Func<NavigationChoice>> GetMenuButtonsActions()
    {
        Dictionary<ConsoleKey, Func<NavigationChoice>> output;

        Dictionary<ConsoleKey, Func<NavigationChoice>> numpadActions;
        Dictionary<ConsoleKey, Func<NavigationChoice>> menuNavActions;

        numpadActions = GetNumKeyActions();
        menuNavActions = GetMenuNavActions();

        output = menuNavActions.Concat(numpadActions)
                    .ToDictionary(
                                     keySelector: kvp => kvp.Key,
                                     elementSelector: kvp => kvp.Value
                                 );
        return output;
    }
    internal void AddKeyFuncAssociation(ConsoleKey key, Action Func, NavigationChoice output = NavigationChoice.stay)
    {
        Func<NavigationChoice> func;

        output = NavigationChoice.stay;
        func = () =>
        {
            Func.Invoke();
            return output;
        };
        _menuButtonsActions.Add(key, func);
    }

    private NavigationChoice Nav(ConsoleKey consoleKey)
    {
        try
        {
            SelectPosition(consoleKey);
        }
        catch (ArgumentOutOfRangeException)
        {
            _actions.SelectFunction(0);
        }
        return NavigationChoice.stay;
    }
    private void SelectPosition(ConsoleKey consoleKey) // MENU *
    {
        int index;
        index = FindIndexOf_OrGetLast(consoleKey);
        _actions.SelectFunction(index);
    }
    private int FindIndexOf_OrGetLast(ConsoleKey consoleKey)
    {
        int lastIndex;
        bool isTooBigInt;
        int actionId;
        string desiredKey;
        int index = 0;

        lastIndex = _actions._commandList.Count - 1;
        isTooBigInt = (int)consoleKey > lastIndex;
        actionId = !isTooBigInt ? (int)consoleKey : lastIndex;

        desiredKey = (from KeyValuePair<string, Action> x in _actions._commandList
                      where x.Key.StartsWith($"{actionId}")
                      select x.Key)
                      .FirstOrDefault()
                      ?? _actions._commandList.Last().Key;

        index = _actions._commandList.Keys.ToList().IndexOf(desiredKey);
        if (index > _actions._commandList.Count)
        {
            index = lastIndex;
        }

        return index;
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

    // --- Menu Concrete Actions 
    private Dictionary<ConsoleKey, Func<NavigationChoice>> GetMenuNavActions()
    {
        Dictionary<ConsoleKey, Func<NavigationChoice>> output;
        output = new Dictionary<ConsoleKey, Func<NavigationChoice>>()
            {
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
        return output;
    }
    private Dictionary<ConsoleKey, Func<NavigationChoice>> GetNumKeyActions()
    {
        Dictionary<ConsoleKey, Func<NavigationChoice>> output;
        Func<int, ConsoleKey> getNumpadKey;
        Func<int, Func<NavigationChoice>> getNavChoice;

        IEnumerable<int> range;

        getNumpadKey = (i) =>
       {
           ConsoleKey output;
           Type keyType;
           string value;

           keyType = typeof(ConsoleKey);
           value = $"NumPad{i}";
           output = (ConsoleKey)Enum.Parse(keyType, value);

           return output;
       };

        getNavChoice = (i) =>
        {
            Func<NavigationChoice> output;
            output = () => Nav((ConsoleKey)i);
            return output;
        };

        range = Enumerable.Range(0, count: 10); // 0..9
        output = range.ToDictionary(getNumpadKey, getNavChoice);

        return output;
    }
    private NavigationChoice SafeNavigate(ConsoleKey keyDown) // MENU *
    {
        NavigationChoice onKeyDown;
        onKeyDown = NavigationChoice.stay;
        try
        {
            onKeyDown = InokeNavigation(keyDown);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message, e);
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
        _actions.LoadedDelegate?.Invoke();
        if (_userKeyDown != NavigationChoice.quit)
        {
            userAct = NavigationChoice.nextMenu;
        }
        return userAct;
    }

    //private NavigationChoice NavDown()
    //{
    //    if (!IsSelected_AtBottom(_actions.SelectedFunctionId))
    //    {
    //        _actions.SelectFunction(_actions.SelectedFunctionId + 1);
    //    }

    //    return NavigationChoice.stay;
    //}
    private NavigationChoice NavDown()
    {
        int tempCounter = 0;
        int prevSel = _actions.SelectedFunctionId;
        bool repeat;
        do
        {
            if (!IsSelected_AtBottom(_actions.SelectedFunctionId))
            {
                tempCounter++;
                _actions.SelectFunction(prevSel + tempCounter);
                repeat = _actions.IsNullSelected();
            }
            else
            {
                _actions.SelectFunction(prevSel);
                break;
            }
        } while (repeat);

        return NavigationChoice.stay;
    }
    private bool IsSelected_AtBottom(int selectedId)
    {
        bool isBottom;
        isBottom = _actions.SelectedFunctionId + 2 > _actions._commandList.Count;
        return isBottom;
    }

    //private NavigationChoice NavUp()
    //{
    //    bool isTop;
    //    isTop = IsSelected_AtTop(_actions.SelectedFunctionId);
    //    if (isTop)
    //    {
    //        _actions.SelectFunction(_actions.SelectedFunctionId - 1);
    //    }
    //    return NavigationChoice.stay;
    //}
    private NavigationChoice NavUp()
    {
        int tempCounter = 0;
        int prevSel = _actions.SelectedFunctionId;
        bool repeat;
        do
        {
            if (!IsSelected_AtTop(_actions.SelectedFunctionId))
            {
                tempCounter--;
                _actions.SelectFunction(prevSel + tempCounter);
                repeat = _actions.IsNullSelected();
            }
            else
            {
                _actions.SelectFunction(prevSel);
                break;
            }
        } while (repeat);

        return NavigationChoice.stay;
    }

    private bool IsSelected_AtTop(int selectedId)
    {
        bool isTop;
        isTop = selectedId == 0;
        return isTop;
    }
}
