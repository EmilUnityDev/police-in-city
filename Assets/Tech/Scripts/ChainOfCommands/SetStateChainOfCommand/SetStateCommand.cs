public class SetStateCommand : ICommand
{
    private IState _stateTo;
    private AbstractStateMachine _stateMachine;

    public SetStateCommand(IState stateTo, AbstractStateMachine stateMachine)
    {
        _stateTo = stateTo;
        _stateMachine = stateMachine;
    }

    public void HandleCommand()
    {
        if (_stateTo != null)
            _stateMachine?.SetState(_stateTo);
    }

    public void SetNext(ICommand next)
    {
        
    }
}