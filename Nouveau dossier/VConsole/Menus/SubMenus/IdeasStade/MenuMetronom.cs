using EpuratedConsole;
using Microsoft.Extensions.DependencyInjection;
using StandardTools.Analysis;
using System;
using System.Threading.Tasks;
using VsConsole;

namespace ConsViSa.Menus.SubMenus;

public class MenuMetronom : AMenu
{
    private Metronom _metronome { get; set; }
    public MenuMetronom(Metronom metronome) : base("Menu Metronome")
    {
        _metronome = metronome;
    }
    public override async void Open()
    {
        IViSaMyClock cloc;
        i = 0;
        cloc = ActivatorUtilities.GetServiceOrCreateInstance<IViSaMyClock>(Program._appHost.Services);
        _metronome = new Metronom(cloc);

        Task openMe;
        Task loop;
        _isLoop = true;
        openMe = new Task(() =>
        {
            _metronome.Tick += DisplayMsgRefreshPage;
            base.Open();
        });

        loop = ToTask(LoopsListener);

        loop.Start();
        openMe.Start();
        openMe.Wait();
        _isLoop = false;
        _metronome.Tick -= DisplayMsgRefreshPage;
        loop.Wait();
        loop.Dispose();
        await openMe;
        openMe.Dispose();
    }

    protected override void SetupCommands()
    {
    }
    private int i = 0;
    private void DisplayMsgRefreshPage2()
    {
        (string, ConsoleColor) newMSg;

        newMSg = GetMessage();
        UpdatesFooterMessages(newMSg);
        this.DisplayPage();
    }
    private void DisplayMsgRefreshPage()
    {
        (string, ConsoleColor) newMSg;

        newMSg = GetMessage();
        //Console.Clear();
        Console.WriteLine(newMSg.Item1,newMSg.Item2);
        
    }

    private (string, ConsoleColor) GetMessage()
    {
        (string, ConsoleColor) newMSg;
        string timeSpans;

        i++;
        //timeSpans = _metronome._clock.GetCurrentTimeSpan().Seconds.ToString();
        timeSpans = i.ToString();
        newMSg = (timeSpans, ConsoleColor.Red);

        return newMSg;
    }
    private void UpdatesFooterMessages((string, ConsoleColor) newMSg)
    {
        if (this._footerItems.Count > 0)
        {
            _footerItems[0] = newMSg;
        }
        else
        {
            this._footerItems.Add(newMSg);
        }
    }

    private void LoopsListener()
    {
        while (_isLoop)
        {
            _metronome.InvokesAction_WheenNeeded();
        }
    }
    private bool _isLoop = true;
    private Task ToTask(Action action)
    {
        return new Task(action);
    }
}
