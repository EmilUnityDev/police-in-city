public class CheckStateCommand : ICommand
{
    private ICommand _next;
    
    private IState _stateFrom;
    private IState _currentState;

    public CheckStateCommand(IState stateFrom, IState currentState)
    {        
        _stateFrom = stateFrom;        
        _currentState = currentState;      
    }

    public void HandleCommand()
    {
        if (_stateFrom == _currentState)
        {
            _next?.HandleCommand();
        }
    }

    public void SetNext(ICommand next)
    {
        _next = next;
    }
}