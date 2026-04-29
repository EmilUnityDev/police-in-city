using System;

public class MultipleGenericActionsAction : IAction
{
    private Action[] _actions;

    public MultipleGenericActionsAction(Action[] actions)
    {
        _actions = actions;
    }

    public void Do()
    {
        if (_actions != null)
        {
            int N = _actions.Length;
            for (int i = 0; i < N; i++)
            {
                _actions[i]();
            }
        }
    }
}