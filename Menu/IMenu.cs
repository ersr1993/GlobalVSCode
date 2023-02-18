using VsConsole.Logic.PageConsole;
using System;

namespace VsConsole
{
    public interface IMenu : IPage
    {
        public int SelectedFunctionId { get; set; } 
        void Open();
        void AddCommand(string name, Action myDelegateFunction);
        //int CountCommands();
        //void RefreshSelection(int selectedLine);
        //void InvokeAction();
        void AddFooterMessage(string footerMessage);
        void AddFooterMessage(string errorMessage, ConsoleColor color);
    }
}