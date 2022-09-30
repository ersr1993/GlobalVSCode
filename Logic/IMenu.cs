namespace MyConsMenu
{
    public interface IMenu
    {
        string footer { get; set; }

        void AddCommand(string name, Menu.DelFunction myDelegateFunction);
        //void Display();
        void AddFooterMessage(string footerMessage);
        void Display(string message);
    }
}