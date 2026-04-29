public class PoliceCarDetainState : IState
{
    private PoliceCarDetainController _detainController;
    private PoliceCarSkannerController _scannerController;
    private CarAnimationController _carAnimController;

    public PoliceCarDetainState(PoliceCarDetainController detainController, PoliceCarSkannerController scannerController, CarAnimationController carAnimController)
    {
        _detainController = detainController;
        _scannerController = scannerController;
        _carAnimController = carAnimController;
    }

    public void Enter()
    {
        _carAnimController.StartMoving();
        _detainController.BlockCar();
        _scannerController.gameObject.SetActive(false);
    }

    public void Exit()
    {
        _detainController.StopMovement();
        _carAnimController.StopMoving();
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {

    }
}