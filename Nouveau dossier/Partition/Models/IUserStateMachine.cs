namespace ViSa.Models;
public interface IUserStateMachine
{
    Delegate OnPushNote { get; }
}