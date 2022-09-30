namespace MyConsMenu
{
    public interface IPage
    {
        string title { get; }
        string content { get; }
        string footer { get; }
        void Display(string footer);
    }
}