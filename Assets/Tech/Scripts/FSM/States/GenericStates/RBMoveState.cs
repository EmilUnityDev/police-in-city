public class RBMoveState : IState
{
    private IAction _moveAction;
    private IAction _enterAction, _exitAction;

    public RBMoveState(IAction moveAction = null, IAction enterAction = null, IAction exitAction = null)
    {
        _moveAction = moveAction;
        _enterAction = enterAction;
        _exitAction = exitAction;
    }

    public void Enter()
    {
        _enterAction?.Do();
    }

    public void Exit()
    {
        _exitAction?.Do();
    }

    public void FixedTick()
    {
        _moveAction?.Do();
    }

    public void Tick()
    {
        
    }
}