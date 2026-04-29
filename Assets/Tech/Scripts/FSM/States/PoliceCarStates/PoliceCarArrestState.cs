using UnityEngine;

public class PoliceCarArrestState : PoliceCarInspectActionState
{
    public PoliceCarArrestState(PoliceCarDetainController detainController, RoadDutyOrderController orderController, GameObject sliderCanvas) : base(detainController, orderController, sliderCanvas)
    {
        _carCrimeType = CarCrimeType.ArrestWorthy;
        _duration = 2f;
    }

    public override void Enter()
    {
        base.Enter();
        _car.TrySetState(_car.StopState, _car.ArrestState);
    }
}