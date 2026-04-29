public abstract class AbstractStateMachine
{
    public abstract IState GetCurrentState();
    public abstract void SetState(IState state);
    public abstract void Tick();
    public abstract void FixedTick();
}