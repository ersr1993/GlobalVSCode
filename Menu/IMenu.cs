using VsConsole.Logic.PageConsole;
using System;

namespace VsConsole
{
    public interface IMenu : IPage
    {
        public int SelectedFunctionId { get; set; } 
        void AddCommand(string name, Action myDelegateFunction);
        int CountCommands();
        void AddFooterMessage(string footerMessage);
        void Open();
        //void RefreshSelection(int selectedLine);
        void InvokeAction();
    }
}