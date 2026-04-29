using UnityEngine;

public class PoliceCarFailedState : IState
{
    private GameObject _failedCanvas;

    public PoliceCarFailedState(GameObject failedCanvas)
    {
        _failedCanvas = failedCanvas;
    }

    public void Enter()
    {
        _failedCanvas.SetActive(true);
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