using System;

public class GenericAction : IAction
{
    private Action _action;

    public GenericAction(Action action)
    {
        _action = action;
    }

    public void Do()
    {
        if (_action != null)
            _action();
    }
}