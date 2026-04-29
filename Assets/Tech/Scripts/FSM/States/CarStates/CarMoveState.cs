public class CarMoveState : IState
{
    private IMovementController _movementController;
    private ISetMovementSpeed _setMovementSpeed;
    private ITurnController _turnController;
    private float _speed;
    private CarAnimationController _carAnimController;

    public CarMoveState(IMovementController movementController, ISetMovementSpeed setMovementSpeed, float speed, ITurnController turnController, CarAnimationController carAnimController = null)
    {
        _movementController = movementController;
        _setMovementSpeed = setMovementSpeed;
        _speed = speed;
        _turnController = turnController;
        _carAnimController = carAnimController;
    }

    public void Enter()
    {
        _carAnimController?.StartMoving();
        _setMovementSpeed.SetSpeed(_speed);
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
        
    }
}