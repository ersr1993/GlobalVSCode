using System;
using System.Collections.Generic;

namespace VsConsole.Logic.PageConsole
{
    public interface IPage
    {
        string _title { get; }
        string _body { get; }
        List<(string, ConsoleColor?)> _footerItems { get; }
        void DisplayPage();
        void ClearFooter();
    }
}