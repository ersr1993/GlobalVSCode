namespace VsConsole.Logic.PageConsole
{
    public interface IPage
    {
        string _title { get; }
        string _body { get; }
        string _footer { get; }
        void DisplayPage();
        void ClearFooter();
    }
}