using UnityEngine;
using Cinemachine;

public class SwapCamerasState : IState
{
    public bool TransitionIsOver
    {
        get => _startTime + _transitionTime < Time.time;
    }

    private CinemachineVirtualCamera _from, _to;
    private float _transitionTime;
    private float _startTime;

    public SwapCamerasState(CinemachineVirtualCamera from, CinemachineVirtualCamera to, float transitionTime = 1)
    {
        _from = from;
        _to = to;
        _transitionTime = transitionTime;
    }

    public void Enter()
    {
        _startTime = Time.time;
        _to.Priority = _from.Priority + 1;
    }

    public void Tick()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Exit()
    {
        
    }
}