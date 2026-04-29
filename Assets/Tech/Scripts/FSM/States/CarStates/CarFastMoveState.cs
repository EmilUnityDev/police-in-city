public class CarFastMoveState : IState
{
    private IMovementController _movementController;
    private ISetMovementSpeed _setMovementSpeed;
    private ITurnController _turnController;
    private float _speed;

    public CarFastMoveState(IMovementController movementController, ISetMovementSpeed setMovementSpeed, float speed, ITurnController turnController)
    {
        _movementController = movementController;
        _setMovementSpeed = setMovementSpeed;
        _speed = speed;
        _turnController = turnController;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {
        _movementController.Move();
        _turnController.Turn();
    }

    public void Tick()
    {
        _setMovementSpeed.GainSpeedTo(_speed);
    }
}