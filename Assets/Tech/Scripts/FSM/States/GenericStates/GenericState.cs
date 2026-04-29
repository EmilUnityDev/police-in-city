using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericState : IState
{
    private IAction _enterAction;
    private IAction _tickAction;
    private IAction _fixedTickAction;
    private IAction _exitAction;

    public GenericState(IAction enterAction = null, IAction tickAction = null, IAction fixedTickAction = null, IAction exitAction = null)
    {
        _enterAction = enterAction;
        _tickAction = tickAction;
        _fixedTickAction = fixedTickAction;
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
        _fixedTickAction?.Do();
    }

    public void Tick()
    {
        _tickAction?.Do();
    }
}