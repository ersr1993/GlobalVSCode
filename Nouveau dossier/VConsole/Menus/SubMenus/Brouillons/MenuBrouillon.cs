using System;
using ViSa.Models;
using ViSaGameMechanics;
using VsConsole;

namespace ConsViSa.Menus.SubMenus;
public class MenuBrouillon : AMenu
{
    private const int PERIOD = 3;
    //private Action myAction;
    private OnxPushNote myAction;
    private UserStateMachine _stateMachine { get; init; }
    private EventEmitter _emitter;
    private EventListener _listener;

    public MenuBrouillon(
                UserStateMachine stateMachine,
                EventEmitter emitter,
                EventListener listener
            ) : base("Menu Brouillon")
    {
        _stateMachine = stateMachine;
        _emitter = emitter;
        _listener = listener;
    }
    public override void Open()
    {
        //_stateMachine.myxDelegate += SomeAction;
        base.Open();
        _stateMachine.myxDelegate -= SomeAction;
    }
    protected override void SetupCommands()
    {
        AddCommand(StartScene);
        _emitter.tickTack += SomeAction;
        AddCommand(EnterLoop);
        //AddCommand(Click);
        //AddCommand(Clickx);
    }

    private void StartScene()
    {

    }

    private void EnterLoop()
    {
        bool hasClicked;
        hasClicked = false;
        while (hasClicked)
        {
            hasClicked = IsPushedKey(Console.ReadKey());
        }
    }
    private bool IsPushedKey(ConsoleKeyInfo? key)
    {
        bool output;
        output = key == null ? true : false;
        return output;
    }

    private string SomeAction()
    {
        this.AddFooterMessage("action click executed");
        return "ou";
    }
    private void Click()
    {
        string output;
        output = _stateMachine.myDelegate()
                    .Result;
        AddFooterMessage(output);
    }
    private void Clickx()
    {
        string output;
        output = _stateMachine.myxDelegate();
        AddFooterMessage(output);
    }
}
