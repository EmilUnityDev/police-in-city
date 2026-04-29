using UnityEngine;

public class PedestrianDetainState : IState
{
    private Transform _officerTransform;    
    private Pedestrian _pedestrian;

    public PedestrianDetainState(Pedestrian pedestrian)
    {        
        _pedestrian = pedestrian;
    }

    public void Enter()
    {
        _officerTransform = _pedestrian.OfficerTransform;
        _pedestrian.transform.LookAt(_officerTransform);
        _pedestrian.HasBeenDetained = true; 
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}