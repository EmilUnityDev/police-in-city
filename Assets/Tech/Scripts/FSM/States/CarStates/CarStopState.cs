public class CarStopState : IState
{
    private IMovementController _movementController;
    private ISetMovementSpeed _setMovementSpeed;
    private ITurnController _turnController;
    private float _speed;
    private CarAnimationController _carAnimController;

    public CarStopState(IMovementController movementController, ISetMovementSpeed setMovementSpeed, ITurnController turnController, CarAnimationController carAnimController)
    {
        _movementController = movementController;
        _setMovementSpeed = setMovementSpeed;
        _speed = 0;
        _turnController = turnController;
        _carAnimController = carAnimController;
    }

    public void Enter()
    {
        _carAnimController.StopMoving();
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {
        _movementController.Move();
        _turnController.Center();
    }

    public void Tick()
    {
        _setMovementSpeed.DropSpeedTo(_speed);
    }
}