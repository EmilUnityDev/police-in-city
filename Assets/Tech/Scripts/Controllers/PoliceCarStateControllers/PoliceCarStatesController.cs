using UnityEngine;

public class PoliceCarStatesController : AbstractPoliceCarStateController
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState();
        }
    }

    public override void SetState()
    {
        _from = _policeCar.ScanState;
        _to = _policeCar.TransitionToDetainCameraState;
        base.SetState();
        transform.gameObject.SetActive(false);
    }
}