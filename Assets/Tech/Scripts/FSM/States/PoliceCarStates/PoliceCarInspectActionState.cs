using UnityEngine;

public class PoliceCarInspectActionState : IState
{
    public bool CanTransition
    {
        get => _startTime + _duration < Time.time;
    }

    protected PoliceCarDetainController _detainController;
    protected RoadDutyOrderController _orderController;
    protected GameObject _sliderCanvas;

    protected Car _car;
    protected CarCrimeType _carCrimeType;
    protected float _startTime, _duration;

    public PoliceCarInspectActionState(PoliceCarDetainController detainController, RoadDutyOrderController orderController, GameObject sliderCanvas)
    {
        _detainController = detainController;
        _orderController = orderController;
        _sliderCanvas = sliderCanvas;
    }

    public virtual void Enter()
    {
        _car = _detainController.DetainedCar;

        _sliderCanvas.SetActive(false);
        _startTime = Time.time;
    }

    public virtual void Exit()
    {
        RoadDutyOrderData data = new RoadDutyOrderData(_carCrimeType, _car.CarData);
        _orderController.CheckOrder(data);
    }

    public virtual void FixedTick()
    {
        
    }

    public virtual void Tick()
    {
        
    }
}