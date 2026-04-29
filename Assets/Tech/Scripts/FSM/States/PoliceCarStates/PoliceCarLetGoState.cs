using UnityEngine;

public class PoliceCarLetGoState : PoliceCarInspectActionState
{
    public PoliceCarLetGoState(PoliceCarDetainController detainController, RoadDutyOrderController orderController, GameObject sliderCanvas) : base(detainController, orderController, sliderCanvas)
    {
        _carCrimeType = CarCrimeType.NotGuilty;
        _duration = 2f;
    }

    public override void Enter()
    {
        base.Enter();
        _car.DollyTrackCameraController.Detach();
        _car.Driver.LetGo();
        _car.CloseTrunk();
        _car.TrySetState(_car.StopState, _car.MoveState);

        _sliderCanvas.SetActive(false);
    }
}