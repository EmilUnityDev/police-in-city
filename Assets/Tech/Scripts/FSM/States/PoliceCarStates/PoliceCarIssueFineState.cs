using UnityEngine;

public class PoliceCarIssueFineState : PoliceCarInspectActionState
{
    public PoliceCarIssueFineState(PoliceCarDetainController detainController, RoadDutyOrderController orderController, GameObject sliderCanvas) : base(detainController, orderController, sliderCanvas)
    {
        _carCrimeType = CarCrimeType.FineWorthy;
        _duration = 1f;
    }

    public override void Enter()
    {
        base.Enter();
        _car.Fine();
    }
}