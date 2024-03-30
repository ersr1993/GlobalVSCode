using VsConsole.Logic.PageConsole;
using System;

namespace VsConsole
{
    public interface IMenu : IPage, IDisposable
    {
        int SelectedFunctionId { get; } 

        void Open();
        void AddCommand(string name, Action myDelegateFunction);
        void AddFooterMessage(string footerMessage);
        void AddFooterMessage(string errorMessage, ConsoleColor color);

    }
}