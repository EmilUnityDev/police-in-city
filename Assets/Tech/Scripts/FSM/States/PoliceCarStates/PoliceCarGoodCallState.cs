using UnityEngine;

public class PoliceCarGoodCallState : IState
{
    private GameObject _goodCallCanvas;

    public PoliceCarGoodCallState(GameObject goodCallCanvas)
    {
        _goodCallCanvas = goodCallCanvas;
    }

    public void Enter()
    {
        _goodCallCanvas.SetActive(true);
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