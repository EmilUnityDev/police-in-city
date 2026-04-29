public interface ICommand 
{
    void HandleCommand();
    void SetNext(ICommand next);
}